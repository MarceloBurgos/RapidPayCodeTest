using RapidPay.Domain.Repositories;
using RapidPay.Infrastructure;
using RapidPay.Infrastructure.Repositories;

namespace RapidPay.Api.ConfigExtensions;

public static class PersistenceConfigurationExtension
{
	public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSingleton<IDataBaseConfigurationProvider, SqlitePersistenceConfigurationProvider>();
		services.AddDbContext<RapidPayContext>();

		services.AddScoped<IGenericRepository<Guid>, GenericRepository>();

		return services;
	}
}
