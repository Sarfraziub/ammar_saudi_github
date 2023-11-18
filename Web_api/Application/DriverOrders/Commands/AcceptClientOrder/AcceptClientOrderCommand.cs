using MediatR;

namespace Application.DriverOrders.Commands.AcceptClientOrder;

public class AcceptClientOrderCommand : IRequest<Unit>
{
    public long ClientOrderId { get; set; }
}