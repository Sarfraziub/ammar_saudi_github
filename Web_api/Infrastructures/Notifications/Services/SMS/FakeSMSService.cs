using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Notifications.Models;
using Infrastructures.Notifications.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Infrastructures.Notifications.Services.SMS;

public class FakeSMSService : ISMSService
{
	private readonly IWebHostEnvironment _webHostEnvironment;

	private readonly IEmailService _emailService;

	// private readonly INotificationService _notification;
	private readonly EmailConfigurations _options;

	public FakeSMSService(
		IWebHostEnvironment webHostEnvironment
		, IEmailService emailService
		// , INotificationService notification
		, IOptions<EmailConfigurations> options)
	{
		_webHostEnvironment = webHostEnvironment;
		_emailService = emailService;
		// _notification = notification;
		_options = options.Value;
	}
	// public FakeSMSService(IOptions<SMSConfigurations> options, IConfiguration configuration)
	// {
	// }


	public async Task<string> SendSmsAsync(SMSMessageDto messageDto)
	{
		var logPath = Path.GetPathRoot("token");
		var logFile = File.Create($"{_webHostEnvironment.WebRootPath}/token.txt");
		var logWriter = new StreamWriter(logFile);
		await logWriter.WriteLineAsync(messageDto.Content);
		await logWriter.DisposeAsync();
		Console.WriteLine($"token: {messageDto.Content}");

		await _emailService.SendEmail(_options.DevTo, "Qatarat OTP", $"{messageDto.Content}");


		return string.Empty;
	}
}
