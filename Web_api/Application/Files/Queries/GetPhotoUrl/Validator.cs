using FluentValidation;

namespace Application.Files.Queries.GetPhotoUrl;

public class Validator : AbstractValidator<GetFileUrlQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


