using RapidPay.Domain.Resources;
using System.Net;

namespace RapidPay.Domain.CustomExceptions;

/// <summary>
/// Represents an exception when an user tries to authenticate using invalid credentials.
/// </summary>
public class InvalidUserException : RapidPayBaseException
{
	/// <summary>
	/// Creates a valid <see cref="InvalidUserException"/> instance.
	/// </summary>
	public InvalidUserException()
	{
		Errors.Add((nameof(ValidationMessages.RP006), ValidationMessages.RP006));
	}

	/// <inheritdoc />
	public override HttpStatusCode ErrorStatusCode => HttpStatusCode.Unauthorized;
}
