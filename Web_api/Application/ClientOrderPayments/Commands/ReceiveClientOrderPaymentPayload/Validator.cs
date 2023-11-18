using FluentValidation;

namespace Application.ClientOrderPayments.Commands.ReceiveClientOrderPaymentPayload;

public class Validator : AbstractValidator<ReceiveClientOrderPaymentPayloadCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
		RuleFor(e => e.PaymentResponse)
			.NotNull().NotEmpty();
	}
}


