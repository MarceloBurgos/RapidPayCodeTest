using Microsoft.EntityFrameworkCore;

namespace RapidPay.Infrastructure;

public class RapidPayContext(DbContextOptions options, IDataBaseConfigurationProvider dataBaseConfigurationProvider) : DbContext(options)
{
	/// <inheritdoc />
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		dataBaseConfigurationProvider.ConfigurePersistenceProvider(optionsBuilder);
	}

	/// <inheritdoc />
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		dataBaseConfigurationProvider.ApplyModelConfiguration(modelBuilder);
	}
}
