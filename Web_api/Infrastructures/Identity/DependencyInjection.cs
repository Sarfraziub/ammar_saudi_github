using System.Text;
using Application.Features.Common.Interfaces;
using Domain;
using Domain.DbModel;
using Infrastructures.Identity.Extensions;
using Infrastructures.Identity.Jwt;
using Infrastructures.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace Infrastructures.Identity;

public static class DependencyInjection
{
	public static IServiceCollection AddUserManagement(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.Configure<JwtConfigurations>(configuration.GetSection("Authentication:Jwt"));
		var jwtTokenConfig = configuration.GetSection("Authentication:Jwt").Get<JwtConfigurations>();
		services.AddSingleton(jwtTokenConfig);


		services.AddScoped<ICurrentUserService, AspUserServices>();
		services.AddScoped<IUserManager, UserManagerService>();
		// services.AddScoped<IRoleManager, RoleManagerService>();
		//services.AddTransient<INotificationService, NotificationService>();

		services.AddTransient<IDateTime, MachineDateTime>();
		//services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));


		services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
			{


			})
			.AddEntityFrameworkStores<ApplicationDbContext>()
			// .AddPasswordValidator<PasswordValidator<ApplicationUser>>()
			.AddDefaultTokenProviders()
			.AddPasswordLessLoginTotpTokenProvider() // Add the custom token provider
			.AddTokenProvider<CustomTwoFactorTokenProvider>(IdentityConstants.TwoFactorUserIdScheme);

		;


		services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
		services.AddHostedService<JwtRefreshTokenCache>();

		//e-mail confirmation TimeSpan
		services.Configure<DataProtectionTokenProviderOptions>(
			options =>
				options.TokenLifespan = TimeSpan.FromMinutes(1)
		);


		services
			.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = true;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = jwtTokenConfig.Issuer,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
					ValidAudience = jwtTokenConfig.Audience,
					ValidateAudience = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.FromMinutes(1)
				};
			});


		services.Configure<IdentityOptions>(options =>
		{
			options.Lockout.MaxFailedAccessAttempts = 10;
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
		});

		services.AddSingleton<ITokenStoreService, TokenStoreService>();

		return services;
	}
}


