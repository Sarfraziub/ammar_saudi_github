namespace Infrastructures.Notifications.Configurations;

public class YamamahSMSConfigurations
{
	public string Endpoint { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public string Tagname { get; set; }
	public string[] AcceptedMobileCodes { get; set; }
}

public class SmsSetting2
{
    public string BaseUrl { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string From { get; set; }
}
