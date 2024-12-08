using RapidPay.Application.CardManagement;
using RapidPay.Application.PaymentFees;
using RapidPay.Domain.ExternalServices;

namespace RapidPay.Api.ConfigExtensions;

public static class DomainServiceConfigurationExtension
{
	public static IServiceCollection AddDomainServices(this IServiceCollection services)
	{
		services.AddSingleton<IUniversalFeesExchangeProvider, HourlyFeeGenerationProvider>();

		services.AddScoped<CardManagementService>();
		services.AddScoped<PaymentTransactionService>();

		return services;
	}
}
