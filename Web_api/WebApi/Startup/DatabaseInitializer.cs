using Application.System.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace WebApi.Startup;

public static partial class DatabaseInitializer
{
	public static async Task<WebApplication> MigrateWithSeedsDatabase(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var services = scope.ServiceProvider;
		try
		{
			var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
			await applicationDbContext.Database.MigrateAsync();


			var mediator = services.GetRequiredService<IMediator>();
			await mediator.Send(new SeedSampleDataCommand(), CancellationToken.None);
		}
		catch (Exception ex)
		{
			var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
			logger.LogError(ex, "An error occurred while migrating or initializing the database");
		}

		return app;
	}
}
