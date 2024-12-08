using System.Net;

namespace RapidPay.Domain.CustomExceptions;

/// <summary>
/// Represents an exception when there is an error during payment transaction process.
/// </summary>
/// <param name="cardNumber"></param>
public class PaymentTransactionException(long cardNumber) : RapidPayBaseException
{
	public long CardNumber { get; } = cardNumber;

	/// <inheritdoc />
	public override HttpStatusCode ErrorStatusCode => HttpStatusCode.BadRequest;
}
