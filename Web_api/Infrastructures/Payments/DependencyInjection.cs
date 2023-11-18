using Application.Features.Common.Interfaces;
using Infrastructures.Payments.Paytaps.Configurations;
using Infrastructures.Payments.Paytaps.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures.Payments;

public static class DependencyInjection
{
	public static IServiceCollection AddPayments(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<PaytapsConfigurations>(configuration.GetSection("Services:Payments:Paytabs"));
		services.Configure<PaytapsWebConfiguration>(configuration.GetSection("Services:Payments:PaytabsWeb"));
		services.AddTransient<IPaymentsService, PaytapsPaymentsService>();
		return services;
	}
}


