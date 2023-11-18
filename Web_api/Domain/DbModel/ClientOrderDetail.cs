namespace Domain.DbModel;

public class ClientOrderDetail : Entity
{
	public ClientOrder ClientOrder { get; set; }
	public long ClientOrderId { get; set; }
	public SaleItem SaleItem { get; set; }
	public long SaleItemId { get; set; }
	public decimal Price { get; set; }
	public int Quantity { get; set; }
}


