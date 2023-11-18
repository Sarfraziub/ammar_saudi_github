using Application.Features.Common.Interfaces;
using Infrastructures.CloudMessaging.WhatsApp.Services;
using Infrastructures.Notifications.Configurations;
using Infrastructures.Notifications.Services.Email;
using Infrastructures.Notifications.Services.SMS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures.Notifications;

public static class DependencyInjection
{
	public static IServiceCollection AddNotifications(this IServiceCollection services,
		IConfiguration configuration)
	{
		// services.AddTransient<INotificationService, NotificationService>();


		services.Configure<EmailConfigurations>(configuration.GetSection("Services:Email"));
		services.Configure<YamamahSMSConfigurations>(configuration.GetSection("Services:YamamahSMS"));
		services.Configure<SmsSetting2>(configuration.GetSection("Services:SmsSetting2"));
		services.AddTransient<IEmailService, SendGridEmailService>();
		services.AddTransient<IWhatsAppService, WhatsAppService>();

		if (bool.Parse(configuration["SMSdevelopment"]))
			services.AddTransient<ISMSService, FakeSMSService>();
		else
			services.AddTransient<ISMSService, YamamahSMSService>();


		return services;
	}
}
