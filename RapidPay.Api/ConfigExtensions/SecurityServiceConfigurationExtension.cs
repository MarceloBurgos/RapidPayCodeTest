using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RapidPay.Application;
using System.Text;

namespace RapidPay.Api.ConfigExtensions;

public static class SecurityServiceConfigurationExtension
{
	public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuthentication(cfg =>
		{
			cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			var jwtKey = configuration.GetSection("Jwt:Key").Get<string>();
			var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();

			options.RequireHttpsMetadata = false;
			options.SaveToken = false;
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)),
				ValidateLifetime = true,
				ValidateIssuer = true,
				ValidIssuer = jwtIssuer,
				ValidateAudience = true,
				ValidAudience = jwtIssuer,
			};
		});

		services.AddScoped<AuthenticationService>();

		return services;
	}
}
