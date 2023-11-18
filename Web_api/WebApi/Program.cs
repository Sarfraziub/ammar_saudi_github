using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using WebApi.Startup;

var builder = WebApplication.CreateBuilder(args);


Console.WriteLine($"Application Name: {builder.Environment.ApplicationName}");
Console.WriteLine($"Environment Name: {builder.Environment.EnvironmentName}");
Console.WriteLine($"ContentRoot Path: {builder.Environment.ContentRootPath}");
Console.WriteLine($"WebRootPath: {builder.Environment.WebRootPath}");


var configuration = builder.Configuration;
var env = builder.Environment;
builder.Configuration.RegisterConfiguration(env, args);

builder.Services.RegisterApplicationServices(configuration);
builder.Services.Configure<FormOptions>(options =>
{
    options.MemoryBufferThreshold = Int32.MaxValue;
    options.MultipartBodyLengthLimit = Int64.MaxValue;
});
var app = builder.Build();
app.UseStaticFiles();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.ConfigureMiddleware(env, configuration, apiVersionDescriptionProvider);
await app.MigrateWithSeedsDatabase();

app.Run();
