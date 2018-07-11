using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PessoasFisicas.Api;
using PessoasFisicas.Api.Controllers;
using PessoasFisicas.Domain;
using PessoasFisicas.Domain.Repositories;
using PessoasFisicas.Domain.Services;
using PessoasFisicas.Infra.EF;
using PessoasFisicas.Infra.EF.Repositories;
using PessoasFisicas.Infra.EF.Services;
using SharedKernel.Common;
using Swashbuckle.AspNetCore.Swagger;

public class Startup
{
	public IConfiguration Configuration { get; }
	public IContainer Container { get; private set; }

	public Startup(IHostingEnvironment env)
	{
		var builder = new ConfigurationBuilder()
			.SetBasePath(env.ContentRootPath)
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
			.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
			.AddEnvironmentVariables();
		Configuration = builder.Build();
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddMvc().AddControllersAsServices();

		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new Info { Title = "Pessoas Fisicas API", Version = "v1" });
			c.DescribeAllEnumsAsStrings();
		});

		services.AddDbContext<PessoasFisicasDbContext>(options =>
		{
			options.UseSqlServer(Configuration.GetConnectionString("AppDatabase"));
			options.UseLazyLoadingProxies();
		});

		var builder = new ContainerBuilder();

		builder.Populate(services);

		ConfigureContainer(builder);

		Container = builder.Build();

	}

	public void Configure(IApplicationBuilder app, IHostingEnvironment env)
	{
		app.UseMvc();
		app.UseSwagger();
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pessoa Física API V1");
		});
	}

	public void ConfigureContainer(ContainerBuilder builder)
	{
		builder.RegisterType<PessoaFisicaController>().PropertiesAutowired();
		builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

		builder.RegisterAssemblyTypes(typeof(Repository<>).Assembly)
			   .Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();

		builder.RegisterAssemblyTypes(typeof(PessoaFisicaService).Assembly)
	   .Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

		builder.RegisterType<AppConnectionString>()
			.AsSelf()
			.WithParameter(new TypedParameter(typeof(string), Configuration.GetConnectionString("AppDatabase")));
	}

}