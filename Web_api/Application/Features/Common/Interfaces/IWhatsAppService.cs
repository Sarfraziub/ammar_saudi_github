
using Application.Features.Common.Models.Notifications.Models;
using Application.Interface;
using Domain.DbModel;

namespace Application.Features.Common.Interfaces
{
    public interface IWhatsAppService
    {
        Task<string> SendMessageAsync(SMSMessageDto messageDto);
        Task<string> SendMessageAsync(List<SMSMessageDto> messageDto);
        Task<string> SendMessageAsync(Dictionary<string, string> settings, SMSMessageDto messageDto);
        Task<string> SendMessageAsync(Dictionary<string, string> settings, List<SMSMessageDto> messageDto);
        string GetEmailBody<T>(T emailBodyModel, string body);
    }
}
