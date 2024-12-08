namespace RapidPay.Application.PaymentFees;

public class HourlyFeeGenerationProvider() : RandomPaymentFeeProvider(TimeSpan.TicksPerHour)
{
}
