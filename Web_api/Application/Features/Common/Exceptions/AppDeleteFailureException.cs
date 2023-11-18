namespace Application.Features.Common.Exceptions;

public class AppDeleteFailureException : Exception
{
	public AppDeleteFailureException(string name, object key, string message)
		: base($"Deletion of entity \"{name}\" ({key}) failed. {message}")
	{
	}

	public AppDeleteFailureException()
	{
	}

	public AppDeleteFailureException(string message) : base(message)
	{
	}

	public AppDeleteFailureException(string message, Exception innerException) : base(message, innerException)
	{
	}
}


