using RapidPay.Domain.Resources;

namespace RapidPay.Domain.CustomExceptions;

/// <summary>
/// Represents an exception when the payment card can not be found in the data base.
/// </summary>
/// <param name="cardNumber">Card number not founded</param>
public class PaymentCardNotFoundException(long cardNumber) : Exception($"{ValidationMessages.RP004}. Card: {cardNumber}")
{
}

public class PaymentTransactionException(long cardNumber) : Exception
{
	public long CardNumber { get; } = cardNumber;

	/// <summary>
	/// Errors during entity validation.
	/// </summary>
	public ICollection<(string Code, string Description)> Errors { get; } = [];
}
