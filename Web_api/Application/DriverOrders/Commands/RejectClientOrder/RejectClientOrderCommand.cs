using MediatR;

namespace Application.DriverOrders.Commands.RejectClientOrder;

public class RejectClientOrderCommand : IRequest<Unit>
{
    public long ClientOrderId { get; set; }
}