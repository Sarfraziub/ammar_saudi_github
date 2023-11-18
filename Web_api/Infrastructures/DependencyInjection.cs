using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//using Infrastructures.Storage.AzureStorage;

namespace Infrastructures;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services,
		IConfiguration configuration)
	{
		Notifications.DependencyInjection.AddNotifications(services, configuration);
		Storage.DependencyInjection.AddStorage(services, configuration);

		// Swagger.DependencyInjection.AddSwagger(services);
		CloudMessaging.Firebase.DependencyInjection.AddFirebase(services, configuration);
		Payments.DependencyInjection.AddPayments(services, configuration);
		return Identity.DependencyInjection.AddUserManagement(services, configuration);
	}
}


