using MediatR;

namespace Application.DriverOrders.Queries.GetUnassignedDriverOrders;

public class GetUnassignedDriverOrdersQuery : IRequest<GetUnassignedDriverOrdersVm>
{
    public string Number { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}