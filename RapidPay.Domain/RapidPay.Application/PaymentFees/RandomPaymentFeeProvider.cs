using RapidPay.Domain.ExternalServices;

namespace RapidPay.Application.PaymentFees;

public abstract class RandomPaymentFeeProvider(long rangeTicks) : IUniversalFeesExchangeProvider
{
	public decimal NextFee()
	{
		lock (lockObject)
		{
			var rangeUpdate = DateTime.UtcNow - lastFeeUpdate;
			if (rangeUpdate.Ticks > _rangeTicks)
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
	private readonly long _rangeTicks = rangeTicks;

	private decimal lastFeeAmount = RandomFee();
	private DateTime lastFeeUpdate = DateTime.UtcNow;
}
