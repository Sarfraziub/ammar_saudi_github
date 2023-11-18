using Application.Features.Common.Models.Notifications.Models;

namespace Application.Features.Common.Interfaces;

public interface ISMSService
{
	Task<string> SendSmsAsync(SMSMessageDto messageDto);
}


