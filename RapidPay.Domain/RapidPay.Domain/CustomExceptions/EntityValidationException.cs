using System.Text;

namespace RapidPay.Domain.CustomExceptions;

/// <summary>
/// Exception to handle any error produced when try to create or update any persistence entity.
/// </summary>
/// <typeparam name="TEntity">Persistence entity type</typeparam>
/// <param name="entity">Entity that cause the failure</param>
public class EntityValidationException<TEntity>(TEntity? entity) : Exception where TEntity : class
{
	/// <summary>
	/// Entity that cause the failure.
	/// </summary>
	public TEntity? Entity { get; } = entity;

	/// <summary>
	/// Errors during entity validation.
	/// </summary>
	public ICollection<(string Code, string Description)> Errors { get; } = [];

	/// <inheritdoc />
	public override string Message
	{
		get
		{
			if (Errors.Count == 0)
			{
				return string.Empty;
			}

			var messages = new StringBuilder();
			messages.AppendLine("Validation errors:");
			foreach (var error in Errors)
			{
				messages.AppendLine(error.ToString());
			}

			return messages.ToString().TrimEnd(Environment.NewLine.ToCharArray());
		}
	}
}
