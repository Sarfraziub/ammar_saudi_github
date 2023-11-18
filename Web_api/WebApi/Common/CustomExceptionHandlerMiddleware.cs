using System.Net;
using Application.Features.Common.Exceptions;
using Newtonsoft.Json;

namespace WebApi.Common;

public class CustomExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public CustomExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		var code = HttpStatusCode.InternalServerError;
		//var errorList = new List<ValidatorError>();
		var result = string.Empty;

		switch (exception)
		{
			case AppValidationException validationException:
				code = HttpStatusCode.BadRequest;
				result = JsonConvert.SerializeObject(validationException.Failures);
				//errorList.AddRange(validationException.Failures.Values.Select(failure => new ValidatorError { Messages = failure}));
				break;
			case AppBadRequestException badRequestException:
				code = HttpStatusCode.BadRequest;
				result = badRequestException.Message;
				//errorList.AddRange(badRequestException.Select(failure => new ValidatorError { Message = failure.ToString() }));
				//errorList.Add(new ValidatorError { Messages = new[] { exception.Message } });
				break;
			case AppNotFoundException _:
				code = HttpStatusCode.NotFound;
				//errorList.AddRange(validationException.Failures.Values.Select(failure => new ValidatorError { Message = failure.ToString() }));
				//errorList.Add(new ValidatorError { Messages = new[] { exception.Message } });
				break;
			default:
				code = HttpStatusCode.BadRequest;
				result = exception.Message;
				//errorList.Add(new ValidatorError {Messages = new[] {exception.Message}});

				break;
		}

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)code;

		if (result == string.Empty) result = JsonConvert.SerializeObject(new { error = exception.Message });

		//var validationError = new ValidatorError
		//{
		//    Id = context.Response.StatusCode,
		//    Message = exception.Message
		//};
		//return context.Response.WriteAsync(JsonConvert.SerializeObject(validationError));
		return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
		//return result;
	}
}


