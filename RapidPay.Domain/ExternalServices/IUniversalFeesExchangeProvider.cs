namespace RapidPay.Domain.ExternalServices;

public interface IUniversalFeesExchangeProvider
{
	decimal NextFee();
}
