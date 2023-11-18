using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApi.Startup.Swagger.Filter;
using WebApi.Startup.Swagger.Options;

namespace WebApi.Startup.Swagger;

public static class DependencyInjection
{
	public static IServiceCollection AddSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(o => ConfigureSwaggerGenOptions(o));
		services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();


		return services;
	}

	private static void ConfigureSwaggerGenOptions(SwaggerGenOptions o)
	{
		// AddSwaggerXmlComments(o);
		o.OperationFilter<SwaggerDefaultValues>();
		o.OperationFilter<HeaderFilter>();
		var securityScheme = new OpenApiSecurityScheme
		{
			Name = "JWT Authentication",
			Description = "Enter JWT Bearer token **_only_**",
			In = ParameterLocation.Header,
			Type = SecuritySchemeType.Http,
			Scheme = "bearer", // must be lower case
			BearerFormat = "JWT",
			Reference = new OpenApiReference
			{
				Id = JwtBearerDefaults.AuthenticationScheme,
				Type = ReferenceType.SecurityScheme
			}
		};
		o.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
		o.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
			{ securityScheme, new string[] { } }
		});
		o.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

	}

	// private static void AddSwaggerXmlComments(SwaggerGenOptions o)
	// {
	// 	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	// 	o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
	// }
}
