namespace Application.Features.Common.Models.Emails;

public class BaseEmailTemplate
{
	public long Id { get; set; }
	public string RequesterEmail { get; set; }
	public string AppUrl { get; set; }
}


