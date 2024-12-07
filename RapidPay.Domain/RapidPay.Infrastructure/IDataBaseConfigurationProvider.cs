using Microsoft.EntityFrameworkCore;

namespace RapidPay.Infrastructure;

/// <summary>
/// Defines the configuration to be applied to <see cref="DbContext"/> instance.
/// </summary>
public interface IDataBaseConfigurationProvider
{
	/// <summary>
	/// Specify the provider to use in <see cref="DbContext"/> creation.
	/// </summary>
	/// <param name="optionsBuilder">Current configuration options for context.</param>
	/// <returns>
	/// Represents the options builder with the custom configurations applied.
	/// </returns>
	DbContextOptionsBuilder ConfigurePersistenceProvider(DbContextOptionsBuilder optionsBuilder);

	/// <summary>
	/// Applies the mapping configuration that will be used by the <see cref="DbContext"/> instance.
	/// </summary>
	/// <param name="modelBuilder">Current model builder used to configure <see cref="DbContext"/>.</param>
	void ApplyModelConfiguration(ModelBuilder modelBuilder);
}
