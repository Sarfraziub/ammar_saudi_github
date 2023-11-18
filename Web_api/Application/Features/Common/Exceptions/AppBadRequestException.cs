using Application.Features.Common.Models.Errors;
using Newtonsoft.Json;

namespace Application.Features.Common.Exceptions;

public class AppBadRequestException : Exception
{
	public AppBadRequestException(string message)
		: base(message)
	{
	}

	public AppBadRequestException(ErrorMessage errorMessage)
		: base(JsonConvert.SerializeObject(errorMessage))
	{
		Id = errorMessage.Id;
		Error = errorMessage.Error;
		ArabicError = errorMessage.ArabicError;
	}

	public AppBadRequestException()
	{
	}

	public AppBadRequestException(string message, Exception innerException) : base(message, innerException)
	{
	}

	private int Id { get; }
	private string Error { get; }
	private string ArabicError { get; }
}


