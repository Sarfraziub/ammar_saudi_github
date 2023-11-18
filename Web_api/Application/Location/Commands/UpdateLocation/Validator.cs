using Application.Features.Common.Interfaces;
using FluentValidation;

namespace Application.Location.Commands.UpdateLocation;

public class Validator : AbstractValidator<UpdateLocationCommand>
{
	public Validator(IApplicationDbContext context)
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
		RuleFor(e => e.RegionId)
			.NotNull().NotEmpty();
		RuleFor(e => e.Name)
			.NotNull().NotEmpty();
		RuleFor(e => e.ArabicName)
			.NotNull().NotEmpty();
		RuleFor(e => e.Latitude)
			.NotNull().NotEmpty();
		RuleFor(e => e.Longitude)
			.NotNull().NotEmpty();
	}
}


