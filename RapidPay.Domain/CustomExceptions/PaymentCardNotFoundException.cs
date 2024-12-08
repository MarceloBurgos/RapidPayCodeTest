using RapidPay.Domain.Resources;
using System.Net;

namespace RapidPay.Domain.CustomExceptions;

/// <summary>
/// Represents an exception when the payment card can not be found in the data base.
/// </summary>
/// <param name="cardNumber">Card number not founded</param>
public class PaymentCardNotFoundException : RapidPayBaseException
{
	public PaymentCardNotFoundException(long cardNumber)
	{
		Errors.Add((nameof(ValidationMessages.RP004), $"{ValidationMessages.RP004}. Card: {cardNumber}"));
	}

	/// <inheritdoc />
	public override HttpStatusCode ErrorStatusCode => HttpStatusCode.NotFound;
}
