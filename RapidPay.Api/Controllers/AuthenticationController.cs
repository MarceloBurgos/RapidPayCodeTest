using Microsoft.AspNetCore.Mvc;
using RapidPay.Api.ConfigExtensions.Model;
using RapidPay.Application;

namespace RapidPay.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController(AuthenticationService authenticationService) : ControllerBase
{
	[HttpPost]
	public IActionResult Login([FromBody] UserModel model)
	{
		var user = authenticationService.Authenticate(model.Username, model.Password);
		return Ok(authenticationService.GenerateJwtToken(user));
	}
}
