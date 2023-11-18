namespace Domain.DbModel;

public class UserNotification : Entity
{
    public ApplicationUser User { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    //Add Arabic
    public string ArabicTitle { get; set; }
    public string ArabicBody { get; set; }
}
