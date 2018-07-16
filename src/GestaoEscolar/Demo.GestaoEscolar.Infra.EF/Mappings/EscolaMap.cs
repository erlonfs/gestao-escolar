using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.GestaoEscolar.Infra.EF.Mappings
{
	public class EscolaMap : IEntityTypeConfiguration<Escola>
	{
		public void Configure(EntityTypeBuilder<Escola> builder)
		{
			builder.ToTable("Escola", "GES");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.DataCriacao);
			builder.Property(x => x.Nome);

		}
	}
}
