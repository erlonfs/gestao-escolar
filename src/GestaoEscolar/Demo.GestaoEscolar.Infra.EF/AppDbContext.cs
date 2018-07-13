using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Demo.GestaoEscolar.Infra.EF
{
	public class AppDbContext : DbContext
	{
		public DbSet<PessoaFisica> PessoaFisica { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			AddMappingsDynamically(modelBuilder);
		}

		private void AddMappingsDynamically(ModelBuilder modelBuilder)
		{
			var currentAssembly = typeof(AppDbContext).Assembly;
			var mappings = currentAssembly.GetTypes().Where(t => t.FullName.StartsWith("Demo.GestaoEscolar.Infra.EF.Mappings") && t.FullName.EndsWith("Map"));

			foreach (var map in mappings.Select(Activator.CreateInstance))
			{
				modelBuilder.ApplyConfiguration((dynamic)map);
			}
		}
	}
}