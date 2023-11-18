using System.Net;
using Application.Features.Common.Interfaces;
using Infrastructures.Notifications.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructures.Notifications.Services.Email;

public class SendGridEmailService : IEmailService
{
	private readonly IConfiguration _configuration;
	private readonly bool _developmentEnv;
	private readonly SendGridClient _sendGridClient;

	public SendGridEmailService(
		IOptions<EmailConfigurations> options
		, IConfiguration configuration
	)
	{
		_configuration = configuration;
		Options = options.Value;
		// _fromEmail = new EmailAddress(_configuration["Services:Username:from"]);
		// _toDevEmail = new EmailAddress(_configuration["Services:Username:devTo"]);
		_sendGridClient = new SendGridClient(Options.Key);
		// _appUrl = _configuration["Services:Username:appUrl"];
		//_emailTemplateService = emailTemplateService;
		_developmentEnv = bool.Parse(_configuration["development"]);
	}

	private EmailConfigurations Options { get; }


	public async Task SendEmail(string[] to, string subject, string body)
	{
		var emails = new List<EmailAddress>();


		if (_developmentEnv)
			emails.Add(new EmailAddress(new EmailAddress(Options.DevTo).Email));
		else
			emails.AddRange(to.Select(item => new EmailAddress(item)));

		var msg = MailHelper.CreateSingleEmailToMultipleRecipients(new EmailAddress(Options.From), emails, subject,
			null, body);
		await _sendGridClient.SendEmailAsync(msg).ConfigureAwait(false);
	}

	public async Task SendEmail(string to, string subject, string body)
	{
		var emails = new List<EmailAddress>
		{
			_developmentEnv
				? new EmailAddress(new EmailAddress(Options.DevTo).Email)
				: new EmailAddress(to)
		};


		var msg = MailHelper.CreateSingleEmailToMultipleRecipients(new EmailAddress(Options.From), emails, subject,
			null, body);

		var response = await _sendGridClient.SendEmailAsync(msg).ConfigureAwait(false);
		if (response.StatusCode != HttpStatusCode.Accepted)
			throw new Exception("Username not sent");
	}

	public async Task SendEmailToDev(string subject, string body)
	{
		var emails = new List<EmailAddress>
		{
			new EmailAddress(new EmailAddress(Options.DevTo).Email)
		};


		var msg = MailHelper.CreateSingleEmailToMultipleRecipients(new EmailAddress(Options.From), emails, subject,
			null, body);

		var response = await _sendGridClient.SendEmailAsync(msg).ConfigureAwait(false);
		if (response.StatusCode != HttpStatusCode.Accepted)
			throw new Exception("Username not sent");
	}
}
