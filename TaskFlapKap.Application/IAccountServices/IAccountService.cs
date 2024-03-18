using TaskFlapKap.DataTransfareObject.Servicedto;
using TaskFlapKap.Domain.Model;

namespace TaskFlapKap.Application.IAccountServices
{
	public interface IAccountService
	{
		Task<UserResponse> LoginAsync(Login dto);
		Task<UserResponse> RegisterAsync(Register dto);
		Task<UserResponse> UpdateAsync(string id, UpdateUserdto dto);
		Task<string> ConfirmEmailAsync(string userId, string code);
		Task<string> ResetPasswordAsync(string email, string newPassword, string code);
		Task<string> SendResetPasswordCodeAsync(string email);
		Task<string> UserIdAsync();
		Task<User> GetUserAsync();
		Task<List<string>> GetUserRolesAsync(User user);
	}
}
