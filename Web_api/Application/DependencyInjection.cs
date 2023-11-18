using System.Reflection;
using Application.Features.Common.Behaviors;
using Application.Features.Common.Interfaces;
using Application.Interface;
using Application.Interface.Context;
using Application.Services;
using Application.Services.Context;
using Application.SieveConfiguration;
using Application.System.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sieve.Models;
using Sieve.Services;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		services.AddMediatR(Assembly.GetExecutingAssembly());
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestUnhandledExceptionBehaviour<,>));
		services.AddTransient<IErrorMessagesService, ErrorMessagesService>();
        services.Configure<SieveOptions>(configuration.GetSection(nameof(SieveOptions)));
        services.AddScoped<ICancellationTokenContext, CancellationTokenContext>();
        services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();
        services.AddScoped<IRequestContext, RequestContext>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        return services;
	}
}


