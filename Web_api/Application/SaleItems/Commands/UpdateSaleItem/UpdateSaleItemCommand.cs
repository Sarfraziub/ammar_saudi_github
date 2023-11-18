using MediatR;

namespace Application.SaleItems.Commands.UpdateSaleItem;

public class UpdateSaleItemCommand : IRequest<Unit>
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string ArabicName { get; set; }
	public string Specifications { get; set; }
	public string ArabicSpecifications { get; set; }
	public decimal Price { get; set; }
	public long ImageId { get; set; }
	public long? PackageId { get; set; }
    public int SalesItemQuantity { get; set; }


}


