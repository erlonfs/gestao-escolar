using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.GestaoEscolar.Infra.EF.Mappings
{
	public class SalaMap : IEntityTypeConfiguration<Sala>
	{
		public void Configure(EntityTypeBuilder<Sala> builder)
		{
			builder.ToTable("Sala", "GES");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.FaseAno);
			builder.Property(x => x.TurnoId);
			builder.Property(x => x.EscolaId);
			builder.HasMany(x => x.Alunos).WithOne(x => x.Sala);
		}
	}
}
