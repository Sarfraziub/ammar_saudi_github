using System.Text.Json;
using Application.Features.Common.Interfaces;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Infrastructures.CloudMessaging.Firebase.Configurations;
using Infrastructures.CloudMessaging.Firebase.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures.CloudMessaging.Firebase;

public static class DependencyInjection
{
	public static IServiceCollection AddFirebase(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<FirebaseConfigurations>(configuration.GetSection("Services:CloudMessaging:Firebase"));
		// services.Configure<FirebaseKeyConfigurations>(configuration.GetSection("Services:CloudMessaging:Firebase:Key"));
		var firebaseKey = configuration.GetSection("Services:CloudMessaging:Firebase:Key")
			.Get<FirebaseKeyConfigurations>();
		FirebaseApp.Create(new AppOptions
		{
			Credential = GoogleCredential.FromJson(JsonSerializer.Serialize(firebaseKey))
		});
		services.AddTransient<IFirebaseService, FirebaseService>();
		return services;
	}
}


