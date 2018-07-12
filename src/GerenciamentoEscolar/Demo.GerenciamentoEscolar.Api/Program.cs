using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Demo.GerenciamentoEscolar.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
				.UseKestrel()
				.ConfigureServices(services => services.AddAutofac())
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}