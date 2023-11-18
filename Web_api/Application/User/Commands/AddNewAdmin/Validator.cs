using FluentValidation;

namespace Application.User.Commands.AddNewAdmin;

public class Validator : AbstractValidator<AddNewAdminCommand>
{
	public Validator()
	{
		RuleFor(e => e.PhoneNumber)
			.NotEmpty();
	}
}


