using Demo.GestaoEscolar.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.GestaoEscolar.Infra.EF.Mappings
{
	public class CpfMap : IEntityTypeConfiguration<Cpf>
	{
		public void Configure(EntityTypeBuilder<Cpf> builder)
		{
			
		}
	}
}
