using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Alunos.Domain.Aggregates;

namespace Alunos.Infra.EF.Mappings
{
	public class AlunoMap : IEntityTypeConfiguration<Aluno>
	{
		public void Configure(EntityTypeBuilder<Aluno> builder)
		{
			builder.ToTable("Aluno", "ALUN");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.DataCriacao);
			builder.Property(x => x.Matricula);
			builder.Property(x => x.PessoaFisicaId);
		
		}
	}
}
