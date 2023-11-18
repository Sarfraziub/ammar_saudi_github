namespace Domain.DbModel;

public class ClientOrderDeliverImage : Entity
{
	public ClientOrder ClientOrder { get; set; }
	public long ClientOrderId { get; set; }
	public File File { get; set; }
	public long FileId { get; set; }
}
