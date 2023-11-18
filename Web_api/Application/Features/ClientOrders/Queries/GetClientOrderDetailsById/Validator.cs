using FluentValidation;

namespace Application.Features.ClientOrders.Queries.GetClientOrderDetailsById;

public class Validator : AbstractValidator<GetClientOrderDetailsByIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


