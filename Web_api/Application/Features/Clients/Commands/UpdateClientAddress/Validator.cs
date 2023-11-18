using FluentValidation;

namespace Application.Features.Clients.Commands.UpdateClientAddress;

public class Validator : AbstractValidator<UpdateClientAddressCommand>
{
	public Validator()
	{
		RuleFor(e => e.Street)
			.NotEmpty().NotNull();
		RuleFor(e => e.City)
			.NotEmpty().NotNull();
		RuleFor(e => e.State)
			.NotEmpty().NotNull();
		RuleFor(e => e.CountryId)
			.NotEmpty().NotNull();
		RuleFor(e => e.ZipCode)
			.NotEmpty().NotNull();
	}
}


