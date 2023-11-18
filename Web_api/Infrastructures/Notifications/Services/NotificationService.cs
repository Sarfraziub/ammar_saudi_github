// using Application.Common.Interfaces;
// using Application.Common.Models.Notifications.Models;
//
// namespace Infrastructures.Notifications.Services;
//
// public class NotificationService : INotificationService
// {
// 	private readonly IEmailService _emailService;
// 	private readonly ISMSService _smsService;
//
// 	public NotificationService(IEmailService emailService, ISMSService smsService)
// 	{
// 		_emailService = emailService;
// 		_smsService = smsService;
// 	}
//
// 	public async Task SendEmailAsync(MessageDto message)
// 	{
// 		await _emailService.SendEmail(message.To, message.Subject, message.Body);
// 	}
//
// 	public async Task<string> SendSmsAsync(SMSMessageDto message)
// 	{
// 		return await _smsService.SendSmsAsync(message);
// 	}
//
// 	// public Task<string> Send(MessageDto emailMessage, SMSMessageDto sMSMessageDto)
// 	// {
// 	// 	throw new NotImplementedException();
// 	// }
// }
//
//
