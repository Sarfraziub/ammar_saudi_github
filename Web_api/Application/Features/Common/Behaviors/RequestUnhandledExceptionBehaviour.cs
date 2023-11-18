using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Common.Behaviors;

public class RequestUnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : notnull
{
	#region 05. Private variables

	private readonly ILogger<TRequest> _logger;

	#endregion

	public RequestUnhandledExceptionBehaviour(ILogger<TRequest> logger)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	#region 02. Actions

	public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
		RequestHandlerDelegate<TResponse> next)
	{
		try
		{
			return await next();
		}
		catch (Exception ex)
		{
			var requestName = typeof(TRequest).Name;

			_logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}",
				requestName, request);

			throw;
		}
	}

	#endregion
}


