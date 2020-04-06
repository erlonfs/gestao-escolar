using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Demo.GestaoEscolar.Domain.Test.Doubles;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Demo.GestaoEscolar.Domain.Test.Aggregates
{
	public class AlunoTest
	{
		private Aluno _aggregate;

		private readonly Guid _alunoId = Guid.NewGuid();
		private readonly int _matricula = 4267;

		public AlunoTest()
		{

		}

		[Fact]
		public void criar_aluno__com_todos_os_parametros__deve_atribuir_os_valores_e_situacao()
		{

			var pessoaFisica = PessoaFisicaStub.PessoaMenorDeIdade;
			var responsavel = PessoaFisicaStub.PessoaMaiorDeIdade;

			_aggregate = new Aluno(_alunoId, pessoaFisica, responsavel, _matricula);

			_aggregate.EntityId.Should().Be(_alunoId);
			_aggregate.Matricula.Should().Be(_matricula);
			_aggregate.PessoaFisica.EntityId.Should().Be(pessoaFisica.EntityId);
			_aggregate.Responsavel.EntityId.Should().Be(responsavel.EntityId);
			_aggregate.DataCriacao.Date.Should().Be(DateTime.Today);
			_aggregate.SituacaoId.Should().Be((int)AlunoSituacao.Matriculado);
		}

		[Fact]
		public void transferir_aluno__com_todos_os_parametros__deve_constar_nova_situacao()
		{
			var pessoaFisica = PessoaFisicaStub.PessoaMenorDeIdade;
			var responsavel = PessoaFisicaStub.PessoaMaiorDeIdade;

			_aggregate = new Aluno(_alunoId, pessoaFisica, responsavel, _matricula);
			_aggregate.Responsavel.EntityId.Should().Be(responsavel.EntityId);

			_aggregate.Transferir();
			_aggregate.SituacaoId.Should().Be((int)AlunoSituacao.Transferido);
		}


		[Fact]
		public void rematricular_aluno__com_todos_os_parametros__deve_constar_nova_situacao_e_responsavel()
		{

			var pessoaFisica = PessoaFisicaStub.PessoaMenorDeIdade;
			var responsavel = PessoaFisicaStub.PessoaMaiorDeIdade;
			var responsavel2 = PessoaFisicaStub.PessoaMaiorDeIdade;

			_aggregate = new Aluno(_alunoId, pessoaFisica, responsavel, _matricula);
			_aggregate.Responsavel.EntityId.Should().Be(responsavel.EntityId);

			_aggregate.Transferir();
			_aggregate.SituacaoId.Should().Be((int)AlunoSituacao.Transferido);

			_aggregate.Rematricular(responsavel2);

			_aggregate.Responsavel.EntityId.Should().Be(responsavel2.EntityId);
			_aggregate.SituacaoId.Should().Be((int)AlunoSituacao.Matriculado);

		}

	}
}
