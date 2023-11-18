namespace Domain.DbModel;

public class ClientOrderLog : Entity
{
	public ClientOrder ClientOrder { get; set; }
	public long ClientOrderId { get; set; }
	public ClientOrderActionLogStatuses ClientOrderActionLogStatus { get; set; }
	public string Description { get; set; }
}


