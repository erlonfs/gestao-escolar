using Microsoft.EntityFrameworkCore;
using PessoasFisicas.Domain.Aggregates;
using System;
using System.Linq;

namespace PessoasFisicas.Domain
{
	public class PessoasFisicasDbContext : DbContext
	{
		public DbSet<PessoaFisica> PessoaFisica { get; set; }

		public PessoasFisicasDbContext(DbContextOptions<PessoasFisicasDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			AddMappingsDynamically(modelBuilder);
		}

		private void AddMappingsDynamically(ModelBuilder modelBuilder)
		{
			var currentAssembly = typeof(PessoasFisicasDbContext).Assembly;
			var mappings = currentAssembly.GetTypes().Where(t => t.FullName.StartsWith("PessoasFisicas.Domain.Mapping.") && t.FullName.EndsWith("Map"));

			foreach (var map in mappings.Select(Activator.CreateInstance))
			{
				modelBuilder.ApplyConfiguration((dynamic)map);
			}
		}
	}
}
