using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskFlapKap.Application.IAccountServices;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Servicedto;
using TaskFlapKap.Domain.Model;
using TaskFlapKap.Presistance.Helper;

namespace TaskFlapKap.Presistance.AccountServices
{
	public class AccountService : IAccountService
	{
		private readonly IUnitOfWork dbContext;
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IEmailService _emailService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly Jwt _jwt;

		public AccountService(IUnitOfWork dbContext, UserManager<User> userManager, IOptions<Jwt> jwt, IHttpContextAccessor httpContextAccessor, IEmailService emailService, SignInManager<User> signInManager)
		{
			this.dbContext = dbContext;
			this.userManager = userManager;
			_jwt = jwt.Value;
			_httpContextAccessor = httpContextAccessor;
			_emailService = emailService;
			_signInManager = signInManager;
		}
		public async Task<UserResponse> LoginAsync(Login dto)
		{
			var user = await userManager.FindByNameAsync(dto.UserName);

			var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, true);

			await dbContext.CommitAsync();
			if (!result.Succeeded)
				return new UserResponse { Massage = $"{result}" };
			//if (user is null || !await userManager.CheckPasswordAsync(user, dto.Password))
			//	return new UserResponse { Massage = "Email Or Password InValed!" };
			if (!user.EmailConfirmed)
				return new UserResponse { Massage = "Please Confirm Your Email!" };

			var createToken = await CreateAccessTokenAsync(user);
			return new UserResponse
			{
				Accesstoken = (new JwtSecurityTokenHandler().WriteToken(createToken)),
				Deposit = user.Deposit,
				IsAuthenticated = true,
				Role = (await userManager.GetRolesAsync(user)).First(),
				UserName = dto.UserName,
			};
		}

		public async Task<UserResponse> RegisterAsync(Register dto)
		{
			User user = new User
			{
				UserName = dto.UserName,
				Email = $"{dto.UserName}@gmail.com",
				Deposit = 0,
				Role = dto.Role,
			};

			var result = await userManager.CreateAsync(user, dto.Password);
			if (!result.Succeeded)
			{
				var errors = new StringBuilder();
				foreach (var error in result.Errors)
					errors.Append(error.Description);
				return new UserResponse { Massage = errors.ToString() };
			}

			var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
			var request = _httpContextAccessor.HttpContext.Request;

			var querystring = request.QueryString.Add("code", code);
			var url = $"{request.Scheme}://{request.Host}/api/User/ConfirmEmail{querystring}&userId={user.Id}";
			var requestMessage = $"To Confirm Email Click Link: <a href='{url}'>Confirm</a>";
			var message = await _emailService.SendEmailAsync(user.Email, requestMessage);

			if (message.Equals("Failed", StringComparison.OrdinalIgnoreCase))
			{
				await dbContext.RollbackAsync();
				return new UserResponse { Massage = "Email Not Confirm!" };
			}

			await dbContext.CommitAsync();

			await userManager.AddToRoleAsync(user, dto.Role.ToString());



			var getAccessToken = await CreateAccessTokenAsync(user);
			var token = new JwtSecurityTokenHandler().WriteToken(getAccessToken);


			return new UserResponse
			{
				Accesstoken = token,
				IsAuthenticated = true,
				UserName = dto.UserName,
				Role = dto.Role.ToString(),
				Massage = message
			};
		}
		public async Task<UserResponse> UpdateAsync(string id, UpdateUserdto dto)
		{
			var user = await dbContext.Users.GetAsync(u => u.Id == id, astracking: false);
			user.Deposit = dto.Deposit;
			user.UserName = dto.UserName;
			user.Email = $"{user.Email}@gmail.com";
			var result = await userManager.UpdateAsync(user);
			if (!result.Succeeded)
			{
				var errors = string.Empty;
				foreach (var error in result.Errors)
					errors += $"{error},";
				return new UserResponse { Massage = errors };
			}
			await dbContext.CommitAsync();
			var getAccessToken = await CreateAccessTokenAsync(user);
			var token = new JwtSecurityTokenHandler().WriteToken(getAccessToken);
			return new UserResponse
			{
				Accesstoken = token,
				IsAuthenticated = true,
				Deposit = dto.Deposit,
				UserName = dto.UserName,
				Role = (await userManager.GetRolesAsync(user)).First()
			};
		}
		private async Task<JwtSecurityToken> CreateAccessTokenAsync(User user)
		{
			var userClaims = await userManager.GetClaimsAsync(user);
			var roleUser = await userManager.GetRolesAsync(user);
			var roleUserClaims = new List<Claim>();
			foreach (var role in roleUser)
				roleUserClaims.Add(new Claim(ClaimTypes.Role, role));

			var claims = new List<Claim>
			{
					new Claim(ClaimTypes.Email,user.Email),
				new Claim(ClaimTypes.Name,user.UserName),
				new Claim(ClaimTypes.PrimarySid,user.Id),
			};
			claims.AddRange(roleUserClaims);
			claims.AddRange(userClaims);
			var symetreckey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
			var signCreadential = new SigningCredentials(symetreckey, SecurityAlgorithms.HmacSha256);
			return new JwtSecurityToken(issuer: _jwt.Issuer, audience: _jwt.Audience, claims: claims, signingCredentials: signCreadential, expires: DateTime.Now.AddHours((int)_jwt.AccessTokenExiretionDate));
		}
		public async Task<string> ConfirmEmailAsync(string userId, string code)
		{
			var user = await userManager.FindByIdAsync(userId);
			var result = await userManager.ConfirmEmailAsync(user!, code.Trim());
			if (!result.Succeeded)
			{
				var errors = new StringBuilder();
				foreach (var error in result.Errors)
					errors.Append(error.Description);
				return errors.ToString();
			}

			await dbContext.CommitAsync();
			return "Confirmed";
		}
		public async Task<string> SendResetPasswordCodeAsync(string email)
		{
			var user = await userManager.FindByEmailAsync(email);
			Random random = new Random();
			var code = random.Next(1, 1000000).ToString("D6");
			var decode = Encoding.UTF8.GetBytes(code);
			user.Code = decode;
			var result = await userManager.UpdateAsync(user);
			if (!result.Succeeded)
				return string.Join(", ", result.Errors.Select(e => e.Description));
			await dbContext.CommitAsync();
			var resultsendEmail = await _emailService.SendEmailAsync(email, $"Code To Reset Password {code}");
			return resultsendEmail;
		}
		public async Task<string> ResetPasswordAsync(string email, string newPassword, string code)
		{
			var user = await userManager.FindByEmailAsync(email);
			var decodeUser = Encoding.UTF8.GetString(user!.Code!);
			if (decodeUser != code)
				return "Code InCorrect!";
			var removePassword = await userManager.RemovePasswordAsync(user);
			if (!removePassword.Succeeded)
				return string.Join(", ", removePassword.Errors.Select(e => e.Description));
			var addPassword = await userManager.AddPasswordAsync(user, newPassword);
			if (!addPassword.Succeeded)
				return string.Join(", ", addPassword.Errors.Select(e => e.Description));
			await dbContext.CommitAsync();
			return "ResetPassword";
		}

		public Task<string> UserIdAsync()
		{
			return Task.FromResult(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.PrimarySid))!.Value);
		}

		public async Task<User> GetUserAsync()
		{
			return await userManager.FindByIdAsync(await UserIdAsync()) ?? new User();
		}

		public async Task<List<string>> GetUserRolesAsync(User user) => (await userManager.GetRolesAsync(user)).ToList();
	}
}
