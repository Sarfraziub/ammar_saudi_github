using Application.Features.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Features.Common.Behaviors;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	#region 05. Private variables

	private readonly IEnumerable<IValidator<TRequest>> _validators;

	#endregion

	public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
	{
		_validators = validators;
	}

	#region 02. Actions

	public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
		RequestHandlerDelegate<TResponse> next)
	{
		if (!_validators.Any()) return await next();
		//var context = new ValidationContext(request);
		var context = new ValidationContext<TRequest>(request);

		var validationResults = await Task
			.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
		var failures = validationResults
			.SelectMany(r => r.Errors.Distinct())
			.Where(f => f != null)
			.ToList();
		failures = failures.GroupBy(test => test.ErrorCode)
			.Select(grp => grp.First())
			.ToList();
		if (failures.Count != 0)
			throw new AppValidationException(failures);
		return await next();
	}

	#endregion
}


