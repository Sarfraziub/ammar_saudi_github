using FluentValidation;

namespace Application.Features.ClientOrders.Queries.GetClientOrderImagesById;

public class Validator : AbstractValidator<GetClientOrderImagesByIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


