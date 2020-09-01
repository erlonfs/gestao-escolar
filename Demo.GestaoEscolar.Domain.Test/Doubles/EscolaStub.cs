using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using System;

namespace Demo.GestaoEscolar.Domain.Test.Doubles
{
	public static class EscolaStub
	{
		public static Escola EscolaValida
		{
			get
			{
				return new Escola(Guid.NewGuid(), "Escola de testes");
			}
		}
	}
}
