namespace Domain.DbModel;

public class DriverClaim : Entity
{
	public string DriverClaimNumber { get; set; }
	public ApplicationUser Driver { get; set; }
	public long? DriverId { get; set; }
	public DriverClaimStatuses? DriverClaimStatus { get; set; }
	public ICollection<ClientOrder> ClientOrders { get; set; }

	public File Receipt { get; set; }
	public long? ReceiptId { get; set; }
}
