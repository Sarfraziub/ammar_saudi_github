namespace Domain.DbModel;

public class ContentSetting : Entity
{
	public string Key { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
	public string ArabicContent { get; set; }
	public string Phone { get; set; }
	public string Email { get; set; }
	public string Address { get; set; }
	public string WhatsApp { get; set; }
	public string Facebook { get; set; }
	public string Instagram { get; set; }
	public string Twitter { get; set; }
	public string Snapchat { get; set; }
    public File Image { get; set; }
    public long ImageId { get; set; }

}
