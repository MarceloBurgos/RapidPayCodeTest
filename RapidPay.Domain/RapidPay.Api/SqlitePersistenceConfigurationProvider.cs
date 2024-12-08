using Microsoft.EntityFrameworkCore;
using RapidPay.Infrastructure;

namespace RapidPay.Api;

public class SqlitePersistenceConfigurationProvider(IConfiguration configuration) : IDataBaseConfigurationProvider
{
	public void ApplyModelConfiguration(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(RapidPayContext).Assembly);
	}

	public DbContextOptionsBuilder ConfigurePersistenceProvider(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseLazyLoadingProxies();
		optionsBuilder.UseSqlite(configuration.GetConnectionString("Sqlite"));
		return optionsBuilder;
	}
}
