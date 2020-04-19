namespace Demo.GestaoEscolar.WebApplication.Test.Extensions
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Hosting;

	public static class ConfigurationBuilderExtensions
    {
        private static readonly string[] KnownCiNames;

        static ConfigurationBuilderExtensions()
        {
            KnownCiNames = new[]
                            {
                                "Appveyor",
                                "Travis"
                            };
        }

        public static IConfigurationBuilder AddCiDependentSettings(this IConfigurationBuilder configurationBuilder, string environment)
        {
            var ciName = KnownCiNames.FirstOrDefault(x => Environment.GetEnvironmentVariable(x.ToUpper())?.ToUpperInvariant() == "TRUE");
            configurationBuilder.AddJsonFile($"appsettings.{environment}.{ciName ?? ""}.json", true, false);

            if(Environment.GetEnvironmentVariable("CI_WINDOWS")?.ToUpperInvariant() == "TRUE")
                configurationBuilder.AddJsonFile($"appsettings.{environment}.{ciName ?? ""}.Windows.json", true, false);

            if (Environment.GetEnvironmentVariable("CI_LINUX")?.ToUpperInvariant() == "TRUE")
                configurationBuilder.AddJsonFile($"appsettings.{environment}.{ciName ?? ""}.Linux.json", true, false);

            return configurationBuilder;
        }

        public static IConfigurationBuilder AddDefaultConfigs(
            this IConfigurationBuilder configurationBuilder,
            string configsPath,
            string environmentName)
        {
            return configurationBuilder
                .AddEnvironmentVariables()
                .SetBasePath(configsPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environmentName}.json", false, true);
        }

        public static IConfigurationBuilder AddDefaultConfigs(
            this IConfigurationBuilder configurationBuilder,
            IHostEnvironment hostEnvironment)
        {
            return configurationBuilder.AddDefaultConfigs(hostEnvironment.ContentRootPath, hostEnvironment.EnvironmentName);
        }
    }
}