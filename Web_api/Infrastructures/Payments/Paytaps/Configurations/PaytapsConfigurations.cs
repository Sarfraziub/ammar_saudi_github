namespace Infrastructures.Payments.Paytaps.Configurations;

public class PaytapsConfigurations
{
	public string Enndpoint { get; set; }
	public int ProfileId { get; set; }
	public string Serverkey { get; set; }
	public string ClientKey { get; set; }
	public bool UserIFrame { get; set; }
	public bool UserIFrameReturnTop { get; set; }
	public bool HideShipping { get; set; }
	public string TransactionClass { get; set; }
	public string TransactionType { get; set; }
	public string Currency { get; set; }
	public string RedirectUrl { get; set; }
}


