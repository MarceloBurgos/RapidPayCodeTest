﻿using System.Net;
using System.Text;

namespace RapidPay.Domain.CustomExceptions;

/// <summary>
/// Base exception class with common information for current service.
/// </summary>
public abstract class RapidPayBaseException : Exception
{
	/// <summary>
	/// Http code that represents the wrong response.
	/// </summary>
	public abstract HttpStatusCode ErrorStatusCode { get; }

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