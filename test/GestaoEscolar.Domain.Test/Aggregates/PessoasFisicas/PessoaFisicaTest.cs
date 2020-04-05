using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using Demo.GestaoEscolar.Domain.ValueObjects;
using FluentAssertions;
using System;
using Xunit;

namespace Demo.GestaoEscolar.Domain.Test.Aggregates.PessoasFisicas
{
	public class PessoaFisicaTest
	{
		private PessoaFisica _aggregate;
		private Guid _pessoaFisicaId = Guid.NewGuid();
		private string _nome = "Diego Daniel Moura";
		private string _cpf = "18284353849";
		private string _nomeSocial = "Diego";
		private string _sexo = "M";
		private DateTime _dataNasc = new DateTime(1998, 05, 27);

		private string _nomeAlterado = "Julia Moura";
		private string _nomeSocialAlterado = null;
		private string _sexoAlterado = "F";
		private DateTime _dataNascAlterado = new DateTime(1998, 05, 30);
		private Cpf _cpfAlterado = "20782878300";

		public PessoaFisicaTest()
		{

		}

		[Fact]
		public void Quando_criar_pessoa_fisica()
		{
			_aggregate = new PessoaFisica(_pessoaFisicaId, _nome, _cpf, _nomeSocial, _sexo, _dataNasc);

			_aggregate.EntityId.Should().Be(_pessoaFisicaId);
			_aggregate.Nome.Should().Be(_nome);
			_aggregate.Cpf.ToString().Should().Be(_cpf);
			_aggregate.NomeSocial.Should().Be(_nomeSocial);
			_aggregate.Sexo.Should().Be(_sexo);
			_aggregate.DataNascimento.Should().Be(_dataNasc);
		}

		[Fact]
		public void Quando_alterar_pessoa_fisica()
		{
			_aggregate = new PessoaFisica(_pessoaFisicaId, _nome, _cpf, _nomeSocial, _sexo, _dataNasc);

			_aggregate.Alterar(_nomeAlterado, _nomeSocialAlterado, _sexoAlterado, _dataNascAlterado);

			_aggregate.Nome.Should().Be(_nomeAlterado);
			_aggregate.NomeSocial.Should().Be(_nomeSocialAlterado);
			_aggregate.Sexo.Should().Be(_sexoAlterado);
			_aggregate.DataNascimento.Should().Be(_dataNascAlterado);
		}

		[Fact]
		public void Quando_alterar_cpf_pessoa_fisica()
		{
			_aggregate = new PessoaFisica(_pessoaFisicaId, _nome, _cpf, _nomeSocial, _sexo, _dataNasc);

			_aggregate.AlterarCpf(_cpfAlterado);

			_aggregate.Cpf.Should().Be(_cpfAlterado);

		}
	}
}
