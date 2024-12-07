using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Repositories;
using RapidPay.Infrastructure.Repositories.Base;
using System.Linq.Expressions;

namespace RapidPay.Infrastructure.Repositories;

/// <summary>
/// Represents the repository operations for all the persisted entities.
/// </summary>
/// <remarks>
/// Creates a valid instance that implements <see cref="IGenericRepository{TId}"/>.
/// </remarks>
public class GenericRepository(RapidPayContext context) : EntityFrameworkCoreBaseRepository(context), IGenericRepository<Guid>
{
	/// <inheritdoc />
	public async Task<TEntity?> GetById<TEntity>(Guid id) where TEntity : class
	{
		return await Context.FindAsync<TEntity>(id);
	}

	/// <inheritdoc />
	public async Task<TEntity?> GetBy<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : class
	{
		return await Context.Set<TEntity>().FirstOrDefaultAsync(filterExpression);
	}

	/// <inheritdoc />
	public async Task Save<TEntity>(TEntity entity) where TEntity : class
	{
		Context.Update(entity);
		await Context.SaveChangesAsync();
	}
}
