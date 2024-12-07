using RapidPay.Domain.ExternalServices;

namespace RapidPay.Application.PaymentFees;

public class RandomPaymentFeeProvider : IUniversalFeesExchangeProvider
{
	public RandomPaymentFeeProvider()
	{
		lastFeeUpdate = DateTime.UtcNow;
		lastFeeAmount = RandomFee();
	}

	public decimal NextFee()
	{
		lock (lockObject)
		{
			var rangeUpdate = DateTime.UtcNow - lastFeeUpdate;
			if (rangeUpdate.Ticks > TimeSpan.TicksPerHour)
			{
				lastFeeAmount = lastFeeAmount * RandomFee();
				lastFeeUpdate = DateTime.UtcNow;
			}
		}

		return lastFeeAmount;
	}

	private static decimal RandomFee()
	{
		var random = new Random();
		return Math.Round((decimal)random.NextDouble() * 2, 3);
	}

	private readonly object lockObject = new();

	private decimal lastFeeAmount;
	private DateTime lastFeeUpdate;
}
