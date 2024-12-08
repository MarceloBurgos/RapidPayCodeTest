using System.Linq.Expressions;

namespace RapidPay.Domain.Repositories;

/// <summary>
/// Represents a generic repository for any persisted entity with basic operations.
/// </summary>
/// <typeparam name="TId">Entity type id</typeparam>
public interface IGenericRepository<in TId>
{
	/// <summary>
	/// Get an entity type using a custom filter expression.
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <returns>
	/// Asynchronous get by operation. Only returns if the expression returns one item, either way it throws an exception.
	/// </returns>
	Task<TEntity?> GetBy<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;

	/// <summary>
	/// Saves (add or remove) an entity using the primary key value as a reference.
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <returns>
	/// Asynchronous save operation.
	/// </returns>
	Task Save<TEntity>(TEntity entity) where TEntity : class;

	/// <summary>
	/// Deletes an existing entity using the primary key value as a reference.
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <returns>
	/// Asynchronous delete operation.
	/// </returns>
	Task Delete<TEntity>(TEntity entity) where TEntity : class;

	/// <summary>
	/// Get a list with all the entities.
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <returns>
	/// Asynchronous get list operation of types specified.
	/// </returns>
	Task<IList<TEntity>> ListAll<TEntity>() where TEntity : class;

	/// <summary>
	/// Get a list with all the entities that apply to the <paramref name="filterExpression"/>.
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <returns>
	/// Asynchronous get list operation of types specified. Only returns if the expression match items.
	/// </returns>
	Task<IList<TEntity>> ListBy<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;
}
