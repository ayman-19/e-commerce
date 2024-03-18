using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskFlapKap.Application.IAccountServices;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Presistance.AccountServices;
using TaskFlapKap.Presistance.DbContext;
using TaskFlapKap.Presistance.Repositories;

namespace TaskFlapKap.Presistance.Dependancies
{
	public static class DependancyInjection
	{
		public static IServiceCollection AddPresistance(this IServiceCollection services, string strConnection)
		{
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(strConnection));
			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.SignIn.RequireConfirmedEmail = true;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;
			});

			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<IInventoryRepository, InventoryRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<ICategoryInventoryRepository, CategoryInventoryRepository>();
			return services;
		}
	}
}
