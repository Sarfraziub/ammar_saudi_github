using Application.Features.Common.Interfaces;
using Infrastructures.Storage.AzureStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures.Storage;

public static class DependencyInjection
{
	public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<AzureStorageConfig>(configuration.GetSection("AzureStorageConfig"));
		services.AddTransient<IImageStorageService, AzureStorageService>();

		return services;
	}
}


