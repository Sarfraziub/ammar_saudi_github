namespace Application.Features.Common.Models.SMS;

public class YamamahCallResponse
{
	public int Cost { get; set; }
	public object InvalidMSISDN { get; set; }
	public string MessageID { get; set; }
	public int Status { get; set; }
	public string StatusDescription { get; set; }
}
