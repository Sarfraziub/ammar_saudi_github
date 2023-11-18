using FluentValidation;

namespace Application.Features.ClientOrders.Queries.GetClientOrdersByClientId;

public class Validator : AbstractValidator<GetClientOrdersByClientIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.ClientId)
			.NotNull().NotEmpty();
	}
}


