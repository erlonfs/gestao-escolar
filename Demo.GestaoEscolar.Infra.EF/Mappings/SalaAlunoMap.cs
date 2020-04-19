using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.GestaoEscolar.Infra.EF.Mappings
{
	public class SalaAlunoMap : IEntityTypeConfiguration<SalaAluno>
	{
		public void Configure(EntityTypeBuilder<SalaAluno> builder)
		{
			builder.ToTable("SalaAluno", "GES");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.SalaId);
			builder.Property(x => x.AlunoId);
		}
	}
}
