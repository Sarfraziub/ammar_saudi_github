using Domain.Attribute;

namespace Domain.DbModel;

public class SaleItem : Entity
{
    public long Id { get; }
    public Package Package { get; set; }
	public long? PackageId { get; set; }
	public string Name { get; set; }
	public string ArabicName { get; set; }
	public string Specifications { get; set; }
	public string ArabicSpecifications { get; set; }
	[MultiCurrency]
    public decimal Price { get; set; }
	public File Image { get; set; }
	public long ImageId { get; set; }
	public int SalesItemQuantity { get; set; }
	public ICollection<ClientOrder> ClientOrders { get; set; }
}


