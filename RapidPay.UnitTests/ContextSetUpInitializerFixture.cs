using Microsoft.EntityFrameworkCore;
using RapidPay.Infrastructure;
using Xunit.Abstractions;

namespace RapidPay.UnitTests;

/// <summary>
/// Defines the set up for the <see cref="Context"/> using <see cref="TestPersistenceConfigurationProvider"/>.
/// </summary>
public class ContextSetUpInitializerFixture
{
	/// <summary>
	/// Context that track all the entities from data base.
	/// </summary>
	public RapidPayContext Context { get; private set; }

	/// <summary>
	/// Configures the options builder to build the <see cref="Context"/>.
	/// </summary>
	public void ConfigureAccountManagerContext(ITestOutputHelper output)
	{
		var optionsBuilder = new DbContextOptionsBuilder<RapidPayContext>();

		Context = new RapidPayContext(optionsBuilder.Options, new TestPersistenceConfigurationProvider(output));
		Context.Database.EnsureCreated();
	}
}
