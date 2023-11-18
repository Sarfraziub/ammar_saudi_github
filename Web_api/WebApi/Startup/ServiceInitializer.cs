using Application;
using Application.Features.Common.Interfaces;
using AspNetCoreRateLimit;
using FluentValidation.AspNetCore;
using Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Persistence;
using WebApi.Startup.Swagger;

namespace WebApi.Startup;

public static partial class ServiceInitializer
{
	private static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

	public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddEndpointsApiExplorer();
		services.AddApplication(configuration);
		services.AddHttpClient();
		services.AddInfrastructure(configuration);
		services.AddPersistence(configuration);


		services.AddHealthChecks()
			.AddDbContextCheck<ApplicationDbContext>();

		services.AddHttpContextAccessor();

		services
			.AddControllers().AddFluentValidation(fv =>
				fv.RegisterValidatorsFromAssemblyContaining<IApplicationDbContext>());

		// services.AddRazorPages();

		// Customise default API behaviour
		services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

		services.AddCors(options =>
		{
			options.AddPolicy(MyAllowSpecificOrigins,
				builder =>
				{
					builder
						.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader()
						;
				});
		});

		services.AddApiVersioning(o =>
		{
			o.AssumeDefaultVersionWhenUnspecified = true;
			o.DefaultApiVersion = new ApiVersion(1, 0);
			o.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
				new HeaderApiVersionReader("x-api-version"),
				new MediaTypeApiVersionReader("x-api-version"));
			// o.UseApiBehavior = false;

		});

		services.AddVersionedApiExplorer(o =>
		{
			o.GroupNameFormat = "'v'VVV";
			o.SubstituteApiVersionInUrl = true;
		});
		services.AddSwagger();
        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        services.AddInMemoryRateLimiting();

        return services;
	}
}
