using MediatR;

namespace Application.Features.ClientOrders.Commands.CompleteClientOrderPayment;

public class CompleteClientOrderPaymentCommand : IRequest<Unit>
{
	public long PaymentTryId { get; set; }
}


