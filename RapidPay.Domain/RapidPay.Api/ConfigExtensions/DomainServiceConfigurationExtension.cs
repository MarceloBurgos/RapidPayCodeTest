using RapidPay.Application.CardManagement;
using RapidPay.Application.PaymentFees;
using RapidPay.Domain.ExternalServices;
using RapidPay.Domain.Repositories;
using RapidPay.Infrastructure;
using RapidPay.Infrastructure.Repositories;

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

public static class PersistenceConfigurationExtension
{
	public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<RapidPayContext>();

		services.AddScoped<IGenericRepository<Guid>, GenericRepository>();

		return services;
	}
}
