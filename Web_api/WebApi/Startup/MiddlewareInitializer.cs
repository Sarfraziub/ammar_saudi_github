using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using WebApi.Common;
using WebApi.Startup.Middleware;

namespace WebApi.Startup;

public static partial class MiddlewareInitializer
{
	private static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

	public static WebApplication ConfigureMiddleware(
		this WebApplication app
		, IWebHostEnvironment env
		, IConfiguration configuration
		, IApiVersionDescriptionProvider apiVersionDescriptionProvider
	)
	{
		if (env.IsDevelopment())
			app.UseDeveloperExceptionPage();
		else
			app.UseExceptionHandler(appBuilder =>
			{
				appBuilder.Run(async context =>
				{
					context.Response.StatusCode = 500;
					await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
				});
			});

        app.UseMiddleware<RequestContextResolver>();
		app.UseCustomExceptionHandler();
        app.UseIpRateLimiting();

        app.UseCors(MyAllowSpecificOrigins);

		app.UseHttpsRedirection();
		app.UseSwagger();
		if (bool.Parse(configuration["EnableSwagger"]))
		{
			app.UseSwagger();
			app.UseSwaggerUI(o =>
			{
				foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
				{
					o.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
						$"Qatarat API - {description.GroupName.ToUpper()}");
				}
			});
		}

		app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();
		app.UseEndpoints(endpoints => endpoints.MapControllers());

        return app;
	}
}
