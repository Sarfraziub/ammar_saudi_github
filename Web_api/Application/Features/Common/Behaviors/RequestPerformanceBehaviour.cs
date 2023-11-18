using System.Diagnostics;
using Application.Features.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Common.Behaviors;

public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : notnull
{
	public RequestPerformanceBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
	{
		if (logger == null) throw new ArgumentNullException(nameof(logger));
		if (logger == null) throw new ArgumentNullException(nameof(logger));
		_timer = new Stopwatch();

		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_currentUserService = currentUserService;
	}

	#region 02. Actions

	public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
		RequestHandlerDelegate<TResponse> next)
	{
		_timer.Start();

		var response = await next();

		_timer.Stop();

		if (_timer.ElapsedMilliseconds > 500)
		{
			var name = typeof(TRequest).Name;

			_logger.LogWarning(
				"Application Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
				name, _timer.ElapsedMilliseconds, _currentUserService.Username, request);
		}

		return response;
	}

	#endregion

	#region 05. Private variables

	private readonly ICurrentUserService _currentUserService;
	private readonly ILogger<TRequest> _logger;
	private readonly Stopwatch _timer;

	#endregion
}


