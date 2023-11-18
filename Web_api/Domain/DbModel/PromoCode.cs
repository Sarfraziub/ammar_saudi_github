namespace Domain.DbModel;

public class PromoCode : Entity
{
	public string Code { get; set; }
	public DateTime? Expiry { get; set; }
	public decimal Percentage { get; set; }
	public int ApplicableType { get; set; }
	public ICollection<ClientOrder> ClientOrders { get; set; }
}


