using FluentValidation;

namespace Application.User.Commands.UpdateMyProfile;

public class Validator : AbstractValidator<UpdateMyProfileCommand>
{
    public Validator()
    {
        RuleFor(e => e.Email)
            .EmailAddress().NotNull().NotEmpty();
        RuleFor(e => e.Name)
            .NotNull().NotEmpty();
        RuleFor(e => e.Language)
	        .NotEmpty().NotNull();
    }
}
