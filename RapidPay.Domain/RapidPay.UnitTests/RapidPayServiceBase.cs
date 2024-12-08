using RapidPay.Application.PaymentFees;
using RapidPay.Infrastructure;
using RapidPay.Infrastructure.Repositories;
using Xunit.Abstractions;

namespace RapidPay.UnitTests;

public abstract class RapidPayServiceBase : IClassFixture<ContextSetUpInitializerFixture>
{
	protected RapidPayServiceBase(ITestOutputHelper output, ContextSetUpInitializerFixture fixture)
	{
		fixture.ConfigureAccountManagerContext(output);
		Context = fixture.Context;

		GenericRepository = new(Context);
		CustomTimeFeeExchangeProvider = new FiveSecondsTimeFeeGenerationProvider();

		Output = output;
	}

	protected ITestOutputHelper Output { get; }

	protected RapidPayContext Context { get; }

	protected GenericRepository GenericRepository { get; }

	protected RandomPaymentFeeProvider CustomTimeFeeExchangeProvider { get; }
}
