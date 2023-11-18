using System.Text;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Emails;
using Microsoft.Extensions.Configuration;

namespace Infrastructures.Notifications.Services.Email;

public class EmailTemplateService : IEmailTemplateService
{
	// private readonly IConfiguration _configuration;
	private readonly string _appUrl;

	public EmailTemplateService(IConfiguration configuration)
	{
		_appUrl = configuration["Services:Username:appUrl"];
	}

	public async Task<string> GetNewRequestTemplate(NewRequestEmailTemplate template)
	{
		// var sb = new StringBuilder();
		// sb.Append(template.Id.ToString());
		// _appUrl.Append(template.Id.ToString());
		var content = await GetEmailContent(EmailTemplates.Created).ConfigureAwait(false);
		var sb = new StringBuilder(content);
		sb.Replace("{url}", _appUrl);
		sb.Replace("{id}", template.Id.ToString());
		return sb.ToString();
	}

	public async Task<string> GetNewTaskTemplate(NewTaskEmailTemplate template)
	{
		var content = await GetEmailContent(EmailTemplates.NewTask).ConfigureAwait(false);
		var sb = new StringBuilder(content);
		sb.Replace("{url}", _appUrl);
		sb.Replace("{id}", template.Id.ToString());
		return content;
	}

	public async Task<string> ApprovedEmailTemplate(ApprovedEmailTemplate template)
	{
		var content = await GetEmailContent(EmailTemplates.Approved).ConfigureAwait(false);
		var sb = new StringBuilder(content);
		sb.Replace("{url}", _appUrl);
		sb.Replace("{id}", template.Id.ToString());
		return content;
	}


	public async Task<string> GetRejectedTemplate(RejectedEmailTemplate template)
	{
		var content = await GetEmailContent(EmailTemplates.Rejected).ConfigureAwait(false);
		var sb = new StringBuilder(content);
		sb.Replace("{url}", _appUrl);
		sb.Replace("{id}", template.Id.ToString());
		return content;
	}

	public async Task<EmailOutput> GetEmailDetails(EmailTemplates emailTemplates, long id)
	{
		// _appUrl.Append(id.ToString());
		var emailOutput = new EmailOutput();
		switch (emailTemplates)
		{
			case EmailTemplates.Created:
			{
				emailOutput.Subject = "RAC Mashhad - New Request Created";
				emailOutput.Body = await GetNewRequestTemplate(new NewRequestEmailTemplate
						{ Id = id, AppUrl = _appUrl })
					.ConfigureAwait(false);
				break;
			}

			case EmailTemplates.NewTask:
			{
				emailOutput.Subject = "RAC Mashhad - New Task Created";
				emailOutput.Body = await GetNewTaskTemplate(new NewTaskEmailTemplate { Id = id, AppUrl = _appUrl })
					.ConfigureAwait(false);
				break;
			}
			case EmailTemplates.Approved:
			{
				emailOutput.Subject = "RAC Mashhad - Request Approved";
				emailOutput.Body =
					await ApprovedEmailTemplate(new ApprovedEmailTemplate { Id = id, AppUrl = _appUrl })
						.ConfigureAwait(false);
				break;
			}
			case EmailTemplates.Rejected:
			{
				emailOutput.Subject = "RAC Mashhad - Request Rejected";
				emailOutput.Body =
					await GetRejectedTemplate(new RejectedEmailTemplate { Id = id, AppUrl = _appUrl })
						.ConfigureAwait(false);
				break;
			}
			// break;
		}

		return emailOutput;
	}

	private async Task<string> GetEmailContent(EmailTemplates emailTemplates)
	{
		var path = emailTemplates switch
		{
			EmailTemplates.Created => "./EmailTemplates/Created.html",
			EmailTemplates.NewTask => "./EmailTemplates/NewTask.html",
			EmailTemplates.Approved => "./EmailTemplates/Approved.html",
			EmailTemplates.Rejected => "./EmailTemplates/Rejected.html",
			_ => throw new Exception("Not defined template")
		};

		var templatePath = string.Format(path);
		using var streamReader = new StreamReader(templatePath);
		var line = await streamReader.ReadToEndAsync().ConfigureAwait(false);
		var stringBuilder = new StringBuilder(line);
		return stringBuilder.ToString();
	}
}


