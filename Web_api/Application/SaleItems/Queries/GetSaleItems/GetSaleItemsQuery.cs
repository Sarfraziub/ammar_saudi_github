using MediatR;

namespace Application.SaleItems.Queries.GetSaleItems;

public class GetSaleItemsQuery : IRequest<GetSaleItemsVm>
{
	public string DeviceId { get; set; }
}


