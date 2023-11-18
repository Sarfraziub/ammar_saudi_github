namespace Domain.DbModel;

public class NotificationTemplate : Entity
{
	public string Title { get; set; }
	public string Body { get; set; }
	public ICollection<NotificationTemplateTransaction> NotificationTemplateTransactions { get; set; }
}


