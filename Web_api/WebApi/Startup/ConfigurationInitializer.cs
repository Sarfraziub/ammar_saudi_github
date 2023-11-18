using System.Reflection;

namespace WebApi.Startup;

public static partial class ConfigurationInitializer
{
	public static ConfigurationManager RegisterConfiguration(this ConfigurationManager configuration, IWebHostEnvironment env, string[] args)
	{
		configuration
			.AddJsonFile("appsettings.json", true, true)
			.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
			.AddJsonFile("appsettings.Local.json", true, true);

		var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
		configuration.AddUserSecrets(appAssembly, true);
		configuration.AddEnvironmentVariables();
		configuration.AddCommandLine(args);
		return configuration;
	}

}
