using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RapidPay.Domain.CustomExceptions;
using RapidPay.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RapidPay.Application;

/// <summary>
/// Contains the logic to handle json web token generation.
/// </summary>
public class AuthenticationService(IConfiguration configuration)
{
	/// <summary>
	/// Generates a valid JWT token used for authentication.
	/// </summary>
	public string GenerateJwtToken(User user)
	{
		var secret = configuration.GetSection("Jwt:Key").Get<string>();
		var issuer = configuration.GetSection("Jwt:Issuer").Get<string>();

		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Email, user.Email),
		};

		var securityToken = new JwtSecurityToken(issuer,
			issuer,
			claims,
			expires: DateTime.Now.AddMinutes(5),
			signingCredentials: credentials);

		var tokenHandler = new JwtSecurityTokenHandler();

		return tokenHandler.WriteToken(securityToken);
	}

	/// <summary>
	/// Authenticates a valid user.
	/// </summary>
	/// <exception cref="InvalidUserException"></exception>
	public static User Authenticate(string userName, string password)
	{
		// This is only for testing purpose. Simulates a credentials validation.
		if (userName.Equals("admin") && password.Equals("rapidpay123!"))
		{
			return new User(userName, "admin@rapidpay.com", password);
		}

		throw new InvalidUserException();
	}
}
