using MediatR;

namespace Application.SaleItems.Commands.DeleteSaleItem;

public class DeleteSaleItemCommand : IRequest<Unit>
{
	public long Id { get; set; }
}


