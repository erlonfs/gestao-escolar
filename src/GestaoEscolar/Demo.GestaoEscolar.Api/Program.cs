using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.IO;

namespace Demo.GestaoEscolar.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			string environmentName;

			#if DEBUG
				environmentName = "Development";
			#elif STAGING
				environmentName = "Staging";
			#elif RELEASE
				environmentName = "Production";
			#endif

			var host = new WebHostBuilder()
				.UseKestrel()
				.UseEnvironment(environmentName)
				.ConfigureServices(services => services.AddAutofac())
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.UseNLog()
				.Build();

			var logger = LogManager.GetCurrentClassLogger();

			try
			{
				host.Run();
			}
			catch (Exception exception)
			{
				logger.Error(exception, "Stopped program because of exception");
				throw;
			}
			finally
			{
				LogManager.Shutdown();
			}
			
		}
	}
}