using System.Net;

namespace RapidPay.Domain.CustomExceptions;

/// <summary>
/// Exception to handle any error produced when try to create or update any persistence entity.
/// </summary>
/// <typeparam name="TEntity">Persistence entity type</typeparam>
/// <param name="entity">Entity that cause the failure</param>
public class EntityValidationException<TEntity>(TEntity? entity) : RapidPayBaseException where TEntity : class
{
	/// <summary>
	/// Entity that cause the failure.
	/// </summary>
	public TEntity? Entity { get; } = entity;

	/// <inheritdoc />
	public override HttpStatusCode ErrorStatusCode => HttpStatusCode.BadRequest;
}
