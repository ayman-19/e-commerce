using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;
using TaskFlapKap.Application.Dependancies;
using TaskFlapKap.Application.Midddleware;
using TaskFlapKap.Domain.Model;
using TaskFlapKap.Presistance.DbContext;
using TaskFlapKap.Presistance.Dependancies;
using TaskFlapKap.Presistance.Helper;

namespace TaskFlapKap.presentation
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
			string strConnetion = builder.Configuration.GetConnectionString("myconnection");
			builder.Services.AddDbContextPool<ApplicationDbContext>(op => op.UseSqlServer(strConnetion));
			builder.Services.AddPresistance(strConnetion).AddApplication();
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
			builder.Services.Configure<Jwt>(builder.Configuration.GetSection("jWTSettings"));
			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "FlapKap ", Version = "v1" });
				options.DescribeAllParametersInCamelCase();
				options.InferSecuritySchemes();
			});
			builder.Services.AddSwaggerGen(o =>
			{
				o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter a valid token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});
				o.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type=ReferenceType.SecurityScheme,
									Id="Bearer"
								}
							},
						new string[]{}
							}
						});
			});
			builder.Services.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(o =>
				{
					o.RequireHttpsMetadata = false;
					o.SaveToken = false;
					o.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = builder.Configuration["jWTSettings:Issuer"],
						ValidAudience = builder.Configuration["jWTSettings:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jWTSettings:Key"])),
						ClockSkew = TimeSpan.Zero
					};
				});
			builder.Services.AddFastEndpoints();
			builder.Services.AddControllersWithViews();
			builder.Services.AddLocalization(op => op.ResourcesPath = "");
			builder.Services.Configure<RequestLocalizationOptions>(op =>
			{
				List<CultureInfo> cultures = new()
				{ new CultureInfo("en-US"), new CultureInfo("ar-eg") };
				op.DefaultRequestCulture = new RequestCulture("ar-eg");
				op.SupportedCultures = cultures;
				op.SupportedUICultures = cultures;
			});

			WebApplication? app = builder.Build();
			app.UseSwagger();
			app.UseSwaggerUI();


			var localization = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(localization!.Value);
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMiddleware<ExeptionHandling>();
			app.UseFastEndpoints();

			app.MapControllers();

			app.Run();
		}
	}
}
