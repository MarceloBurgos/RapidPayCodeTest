using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RapidPay.Infrastructure;
using Xunit.Abstractions;

namespace RapidPay.UnitTests;

/// <summary>
/// Defines the Sqlite provider for <see cref="DeductionsContext"/>.
/// </summary>
/// <remarks>
/// Creates a valid <see cref="SqliteConfigurationProvider"/> instance.
/// </remarks>
public class SqliteConfigurationProvider(ITestOutputHelper output) : IDataBaseConfigurationProvider
{
	/// <inheritdoc />
	public DbContextOptionsBuilder ConfigurePersistenceProvider(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseLazyLoadingProxies();
		optionsBuilder
			.EnableSensitiveDataLogging()
			.LogTo(_output.WriteLine, LogLevel.Information);

		var connection = new SqliteConnection("DataSource=:memory:");
		connection.Open();
		return optionsBuilder.UseSqlite(connection);
	}

	/// <inheritdoc />
	public void ApplyModelConfiguration(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(RapidPayContext).Assembly);
	}

	private readonly ITestOutputHelper _output = output;
}