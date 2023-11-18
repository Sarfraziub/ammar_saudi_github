namespace Application.Features.Common.Interfaces;

public interface IEmailService
{
	Task SendEmail(string[] to, string subject, string body);
	Task SendEmail(string to, string subject, string body);
	Task SendEmailToDev(string subject, string body);
}


