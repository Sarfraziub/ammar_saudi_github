namespace Domain.DbModel;

public class ClientOrderPayment : Entity
{
	public ClientOrder ClientOrder { get; set; }
	public long ClientOrderId { get; set; }
	public string Payload { get; set; }
	public string PaymentResponse { get; set; }
	public string? CartId { get; set; }
	public string? TransactionReference { get; set; }
}
