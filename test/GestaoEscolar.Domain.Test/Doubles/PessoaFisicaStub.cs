using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using System;

namespace Demo.GestaoEscolar.Domain.Test.Doubles
{
	public static class PessoaFisicaStub
	{
		public static PessoaFisica PessoaMenorDeIdade
		{
			get
			{
				return new PessoaFisica(Guid.NewGuid(), "Lucca Ricardo Porto",
										"30839452055", "Luccaa", "M", new DateTime(2005, 04, 02));
			}
		}

		public static PessoaFisica PessoaMaiorDeIdade
		{
			get
			{
				return new PessoaFisica(Guid.NewGuid(), "Thiago Julio Martins",
										"21520659725", "Julio", "M", new DateTime(1959, 01, 11));
			}
		}
	}
}
