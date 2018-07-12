using Alunos.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Alunos.Infra.EF
{
	public class AlunosDbContext : DbContext
	{
		public DbSet<Aluno> Aluno { get; set; }
		public DbSet<PessoaFisica> PessoaFisica { get; set; }

		public AlunosDbContext(DbContextOptions<AlunosDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			AddMappingsDynamically(modelBuilder);
		}

		private void AddMappingsDynamically(ModelBuilder modelBuilder)
		{
			var currentAssembly = typeof(AlunosDbContext).Assembly;
			var mappings = currentAssembly.GetTypes().Where(t => t.FullName.StartsWith("Alunos.Infra.EF.Mappings") && t.FullName.EndsWith("Map"));

			foreach (var map in mappings.Select(Activator.CreateInstance))
			{
				modelBuilder.ApplyConfiguration((dynamic)map);
			}
		}
	}
}