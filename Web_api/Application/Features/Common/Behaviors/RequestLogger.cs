using Application.Features.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Features.Common.Behaviors;

public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
	private readonly ICurrentUserService _currentUserService;
	private readonly ILogger _logger;


	public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService)
	{
		_logger = logger;
		_currentUserService = currentUserService;
	}

	public Task Process(TRequest request, CancellationToken cancellationToken)
	{
		var name = typeof(TRequest).Name;

		_logger.LogInformation("Application Request: {Name} {@UserId} {@Request}",
			name, _currentUserService.Username, request);

		return Task.CompletedTask;
	}
}


