using FluentValidation;

namespace Application.Features.ClientOrders.Queries.GetClientOrderById;

public class Validator : AbstractValidator<GetClientOrderByIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


