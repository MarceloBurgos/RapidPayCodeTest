using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RapidPay.Infrastructure.Repositories.Base;

/// <summary>
/// Represents the basic operations for all the repositories.
/// </summary>
public abstract class EntityFrameworkCoreBaseRepository
{
	/// <summary>
	/// Base constructor that operates on <see cref="RapidPayContext"/>.
	/// </summary>
	protected EntityFrameworkCoreBaseRepository(RapidPayContext context)
	{
		Context = context ?? throw new ArgumentNullException(nameof(context));
	}

	/// <summary>
	/// Adds a new entity to <see cref="DeductionsContext"/> to start be tracking.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous add entity operation.
	/// </returns>
	public async Task<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
	{
		await Context.AddAsync(entity);
		await Context.SaveChangesAsync();
		return entity;
	}

	/// <summary>
	/// Removes a tracked entity from <see cref="DeductionsContext"/>.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous delete entity operation.
	/// </returns>
	public async Task Delete<TEntity>(TEntity entity) where TEntity : class
	{
		Context.Remove(entity);
		await Context.SaveChangesAsync();
	}

	/// <summary>
	/// Lists all the entities of a specific type defined in <typeparamref name="TEntity"/>.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous list operation.
	/// </returns>
	public virtual async Task<IList<TEntity>> ListAll<TEntity>() where TEntity : class
	{
		return await Context.Set<TEntity>().ToListAsync();
	}

	/// <inheritdoc />
	public async Task<IList<TEntity>> ListBy<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : class
	{
		return await Context.Set<TEntity>().Where(filterExpression).ToListAsync();
	}

	/// <summary>
	/// Represents the current context from the application instance.
	/// </summary>
	protected RapidPayContext Context { get; }
}
