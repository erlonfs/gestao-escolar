using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using Demo.GestaoEscolar.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.GestaoEscolar.Infra.EF.Mappings
{
	public class PessoaFisicaMap : IEntityTypeConfiguration<PessoaFisica>
	{
		public void Configure(EntityTypeBuilder<PessoaFisica> builder)
		{
			builder.ToTable("PessoaFisica", "GES");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.DataCriacao);
			builder.Property(x => x.Nome);
			builder.Property(x => x.NomeSocial);
			builder.Property(x => x.Sexo);
			builder.Property(x => x.DataNascimento);

			builder.OwnsOne(x => x.Cpf, a =>
			{
				a.Property(p => p.Numero).HasColumnName("Cpf");
			});
		}
	}
}
