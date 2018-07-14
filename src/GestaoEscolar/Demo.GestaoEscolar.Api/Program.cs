using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
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
				.Build();

			host.Run();
		}
	}
}