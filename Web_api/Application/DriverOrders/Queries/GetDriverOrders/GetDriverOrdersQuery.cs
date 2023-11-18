using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Application.DriverOrders.Queries.GetDriverOrders;

public class GetDriverOrdersQuery : IRequest<GetDriverOrdersVm>
{
    public string Number { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public ClientOrderStatuses? ClientOrderStatus { get; set; }
}
