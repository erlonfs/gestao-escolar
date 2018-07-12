using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Alunos.Domain.Aggregates;

namespace Alunos.Infra.EF.Mappings
{
	public class PessoaFisicaMap : IEntityTypeConfiguration<PessoaFisica>
	{
		public void Configure(EntityTypeBuilder<PessoaFisica> builder)
		{
			builder.ToTable("PessoaFisica", "PFIS");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.Nome);
			builder.Property(x => x.Cpf);
			builder.Property(x => x.Sexo);
			builder.Property(x => x.DataNascimento);
		}
	}
}
