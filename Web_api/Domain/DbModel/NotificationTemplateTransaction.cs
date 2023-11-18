namespace Domain.DbModel;

public class NotificationTemplateTransaction : Entity
{
	public NotificationTemplate NotificationTemplate { get; set; }
	public long NotificationTemplateId { get; set; }
	public string Response { get; set; }
}

