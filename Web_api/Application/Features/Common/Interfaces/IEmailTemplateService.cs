using Application.Features.Common.Models.Emails;

namespace Application.Features.Common.Interfaces;

public interface IEmailTemplateService
{
	Task<string> GetNewRequestTemplate(NewRequestEmailTemplate template);
	Task<string> ApprovedEmailTemplate(ApprovedEmailTemplate template);
	Task<string> GetNewTaskTemplate(NewTaskEmailTemplate template);
	Task<string> GetRejectedTemplate(RejectedEmailTemplate template);

	Task<EmailOutput> GetEmailDetails(EmailTemplates emailTemplates, long id);
}


