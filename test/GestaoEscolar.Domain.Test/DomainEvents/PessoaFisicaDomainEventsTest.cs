using Demo.GestaoEscolar.Agregates.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Test.Doubles;
using FluentAssertions;
using System;
using Xunit;

namespace Demo.GestaoEscolar.Domain.Test.DomainEvents
{
	public class PessoaFisicaDomainEventsTest
	{
		[Fact]
		public void pessoa_fisica_criada_deve_constar_todos_os_valores_atribuidos()
		{
			var pessoa = PessoaFisicaStub.PessoaMenorDeIdade;
			var e = new PessoaFisicaCriada(pessoa.EntityId, pessoa);

			e.AggregateId.Should().Be(pessoa.EntityId);
			e.PessoaFisica.EntityId.Should().Be(pessoa.EntityId);
			e.DataExecucao.Date.Should().Be(DateTime.Today);
		}


		[Fact]
		public void pessoa_fisica_alterada_deve_constar_todos_os_valores_atribuidos()
		{
			var pessoa = PessoaFisicaStub.PessoaMenorDeIdade;
			var e = new PessoaFisicaAlterada(pessoa.EntityId, pessoa);

			e.AggregateId.Should().Be(pessoa.EntityId);
			e.PessoaFisica.EntityId.Should().Be(pessoa.EntityId);
			e.DataExecucao.Date.Should().Be(DateTime.Today);
		}

		[Fact]
		public void pessoa_fisica_cpf_alterado_deve_constar_todos_os_valores_atribuidos()
		{
			var pessoa = PessoaFisicaStub.PessoaMenorDeIdade;
			var e = new PessoaFisicaCpfAlterado(pessoa.EntityId, pessoa);

			e.AggregateId.Should().Be(pessoa.EntityId);
			e.PessoaFisica.EntityId.Should().Be(pessoa.EntityId);
			e.DataExecucao.Date.Should().Be(DateTime.Today);
		}
	}
}
