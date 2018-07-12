using Demo.GerenciamentoEscolar.Domain.Aggregates.Alunos;
using Demo.GerenciamentoEscolar.Domain.Aggregates.PessoasFisicas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.GerenciamentoEscolar.Infra.EF.Mappings
{
	public class AlunoMap : IEntityTypeConfiguration<Aluno>
	{
		public void Configure(EntityTypeBuilder<Aluno> builder)
		{
			builder.ToTable("Aluno", "GES");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.DataCriacao);
			builder.Property(x => x.Matricula);
			builder.Property(x => x.PessoaFisicaId);
		}
	}
}
