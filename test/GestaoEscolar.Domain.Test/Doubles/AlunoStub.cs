using AutoFixture;
using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using System;

namespace Demo.GestaoEscolar.Domain.Test.Doubles
{
	public static class AlunoStub
	{
		private static Fixture _fixture = new Fixture();

		public static Aluno AlunoValido
		{
			get
			{
				return new Aluno(Guid.NewGuid(), PessoaFisicaStub.PessoaMenorDeIdade, 
								PessoaFisicaStub.PessoaMaiorDeIdade, _fixture.Create<int>());
			}
		}
	}
}
