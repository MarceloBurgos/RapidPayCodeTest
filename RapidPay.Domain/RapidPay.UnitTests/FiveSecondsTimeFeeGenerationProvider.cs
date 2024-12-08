using RapidPay.Application.PaymentFees;

namespace RapidPay.UnitTests;

public class FiveSecondsTimeFeeGenerationProvider() : RandomPaymentFeeProvider(TimeSpan.FromSeconds(5).Ticks)
{
}
