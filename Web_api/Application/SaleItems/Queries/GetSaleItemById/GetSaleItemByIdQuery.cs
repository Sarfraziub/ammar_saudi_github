using MediatR;

namespace Application.SaleItems.Queries.GetSaleItemById;

public class GetSaleItemByIdQuery : IRequest<GetSaleItemByIdDto>
{
	public long Id { get; set; }
}


