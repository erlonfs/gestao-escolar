using Autofac;
using Autofac.Extensions.DependencyInjection;
using CrossCutting;
using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Finders;
using Demo.GestaoEscolar.Domain.Services.Alunos;
using Demo.GestaoEscolar.Domain.Services.Escolas;
using Demo.GestaoEscolar.Domain.Services.PessoasFisicas;
using Demo.GestaoEscolar.Infra.Dapper.Data;
using Demo.GestaoEscolar.WebApplication.Test.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IO;
using System.Reflection;

namespace Demo.GestaoEscolar.WebApplication.Test
{
	public class ProgramTest<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
	{
		private readonly string _environmentName;
		private readonly string _configsDirectory;

		public IConfigurationRoot Configuration { get; }
		public TimeSpan ApiTimeout { get; }
		public Mock<ILogger> MockLogger { get; }

		private static IConfigurationBuilder ConfigurationSetup(IConfigurationBuilder builder, string configsPath, string environmentName) =>
			builder
				.AddDefaultConfigs(configsPath, environmentName);

		public ProgramTest()
		{
			_environmentName
				= Environment.GetEnvironmentVariable("Hosting:Environment")
				  ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
				  ?? Environments.Development;
			_configsDirectory = Path.GetDirectoryName(typeof(Startup).GetTypeInfo().Assembly.Location);

			Configuration = ConfigurationSetup(new ConfigurationBuilder(), _configsDirectory, _environmentName).Build();
			ApiTimeout = Configuration.GetValue<TimeSpan>("ApiTimeout");

			MockLogger = MockLoggerExtensions.CreateMockLogger();
		}

		protected override IWebHostBuilder CreateWebHostBuilder()
		{
			return WebHost.CreateDefaultBuilder(null)
						  .UseStartup<TEntryPoint>()
						  .ConfigureTestContainer<ContainerBuilder>(builder =>
						  {
							  builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
							  builder.RegisterType<PessoaFisicaService>().As<IPessoaFisicaService>();
							  builder.RegisterType<PessoaFisicaFinder>().As<IPessoaFisicaFinder>();
							  builder.RegisterType<EscolaService>().As<IEscolaService>();
							  builder.RegisterType<EscolaFinder>().As<IEscolaFinder>();
							  builder.RegisterType<AlunoFinder>().As<IAlunoFinder>();
							  builder.RegisterType<AlunoService>().As<IAlunoService>();
							  builder.RegisterType<MessageBusFake>().As<IMessageBus>();

							  builder.RegisterType<DomainEventsBag>().As<IDomainEventsBag>();

							  builder.RegisterType<PessoaFisica>().As<IDomainEventsBag>().PropertiesAutowired();

							  builder.RegisterType<Escola>().PropertiesAutowired();
							  builder.RegisterType<Aluno>().PropertiesAutowired();
						  })
						  .ConfigureServices(services => services.AddAutofac())
						  .UseSolutionRelativeContentRoot("Demo.GestaoEscolar.WebApplication")
						  .ConfigureAppConfiguration((context, conf) =>
						  {
							  var projectDir = Directory.GetCurrentDirectory();
							  var configPath = Path.Combine(projectDir, "appsettings.json");

							  conf.AddJsonFile(configPath);
						  });

		}

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder
				.UseMockLogger(MockLogger)
				.UseEnvironment(_environmentName)
				.ConfigureAppConfiguration(x => ConfigurationSetup(x, _configsDirectory, _environmentName));
		}
	}
}
