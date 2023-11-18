using System.Runtime.Serialization;

namespace Application.Features.Common.Exceptions;

public class AppNotFoundException : Exception
{
	public AppNotFoundException(string name, object key)
		: base($"Entity \"{name}\" ({key}) was not found.")
	{
	}

	public AppNotFoundException()
	{
	}

	protected AppNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	public AppNotFoundException(string message) : base(message)
	{
	}

	public AppNotFoundException(string message, Exception innerException) : base(message, innerException)
	{
	}
}


