using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Domain;
using RapidPay.Domain.CustomExceptions;
using System.Net;

namespace RapidPay.Api.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		var reference = $"Error during '{httpContext.Request.Method}{httpContext.Request.Path}' execution";

		var problemDetails = new ProblemDetails
		{
			Type = $"Exception type '{exception.GetType().Name}'",
			Instance = $"Method: {httpContext.Request.Method}. Path: {httpContext.Request.Path}",
			Title = reference
		};

		switch (exception)
		{
			case RapidPayBaseException rapidPayBaseException:
				httpContext.Response.StatusCode = (int)rapidPayBaseException.ErrorStatusCode;
				httpContext.Response.ContentType = "application/text";
				await httpContext.Response.WriteAsJsonAsync(rapidPayBaseException.Message, JsonSerializerGlobalOptions.SerializerOptions, cancellationToken);

				logger.LogError("Service exception {message}", rapidPayBaseException.Message);
				break;

			default:
				problemDetails.Detail = $"Unexpected exception occurs. {exception.Message}";
				problemDetails.Status = (int)HttpStatusCode.InternalServerError;

				httpContext.Response.StatusCode = problemDetails.Status.Value;
				httpContext.Response.ContentType = "application/json";
				await httpContext.Response.WriteAsJsonAsync(problemDetails, JsonSerializerGlobalOptions.SerializerOptions, cancellationToken);

				logger.LogCritical(exception, "Unexpected exception occurs. {message}", exception.Message);
				break;
		}

		return true;
	}
}
