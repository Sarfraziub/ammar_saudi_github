using FluentValidation.Results;

namespace Application.Features.Common.Exceptions;

public class AppValidationException : Exception
{
	public AppValidationException()
		: base("One or more validation failures have occurred.")
	{
		Failures = new Dictionary<string, string[]>();
	}

	public AppValidationException(IReadOnlyCollection<ValidationFailure> failures)
		: this()
	{
		var propertyNames = failures
			.Select(e => e.PropertyName)
			.Distinct();

		foreach (var propertyName in propertyNames)
		{
			var propertyFailures = failures
				.Where(e => e.PropertyName == propertyName)
				.Select(e =>
					e.ErrorMessage.Replace("\"", string.Empty).Replace("'", string.Empty).TrimStart().TrimEnd())
				.Distinct()
				.ToArray();

			Failures.Add(propertyName, propertyFailures);
		}
	}

	public AppValidationException(string message) : base(message)
	{
	}

	public AppValidationException(string message, Exception innerException) : base(message, innerException)
	{
	}

	public IDictionary<string, string[]> Failures { get; }
}


