using Application.Interface;

namespace Application.Features.Common.Models.Notifications.Models;

public class SMSMessageDto : IMessageDto
{
	public string To { get; set; }
	public string Content { get; set; }
    public string Type { get; set; } = "text";
	public string FileUrl { get; set; } = string.Empty;
}


