using FluentValidation;

namespace Application.ClientOrderPayments.Commands.SendClientOrderPaymentPayload;

public class Validator : AbstractValidator<SendClientOrderPaymentPayloadCommand>
{
	public Validator()
	{
		RuleFor(e => e.ClientOrderId)
			.NotNull().NotEmpty();
		RuleFor(e => e.Payload)
			.NotNull().NotEmpty();
	}
}


