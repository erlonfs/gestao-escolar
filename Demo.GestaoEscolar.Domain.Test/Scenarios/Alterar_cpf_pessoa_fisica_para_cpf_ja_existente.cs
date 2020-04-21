using AutoFixture;
using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Exceptions.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Repositories.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Services.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Test.Doubles;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Demo.GestaoEscolar.Domain.Test.Scenarios
{
	public class Alterar_cpf_pessoa_fisica_para_cpf_ja_existente
	{
		private PessoaFisicaService _service;
		private PessoaFisica _pessoaFisica;
		private PessoaFisica _pessoaFisicaComMesmoCpf;
		private Mock<IPessoaFisicaRepository> _mockPessoaFisicaRepository = new Mock<IPessoaFisicaRepository>();

		public Alterar_cpf_pessoa_fisica_para_cpf_ja_existente()
		{
			_pessoaFisica = PessoaFisicaStub.PessoaMaiorDeIdade;
			_pessoaFisicaComMesmoCpf = PessoaFisicaStub.PessoaMenorDeIdade;

			_mockPessoaFisicaRepository.Setup(x => x.GetByEntityIdAsync(It.IsAny<Guid>()))
				.Returns((Guid entityId) =>
					{
						if (entityId == _pessoaFisica.EntityId) return Task.FromResult(_pessoaFisica);
						return Task.FromResult<PessoaFisica>(null);

					});

			_mockPessoaFisicaRepository.Setup(x => x.ObterPorCpfAsync(It.IsAny<string>()))
				.Returns((string cpf) =>
				{
					if (cpf == _pessoaFisicaComMesmoCpf.Cpf.Numero) return Task.FromResult(_pessoaFisicaComMesmoCpf);
					return Task.FromResult<PessoaFisica>(null);

				});

			_service = new PessoaFisicaService(_mockPessoaFisicaRepository.Object);


		}

		[Fact]
		public void Deve_lancar_exception()
		{
			Action act = () =>
			{
				_service.AlterarCpfAsync(_pessoaFisica.EntityId, _pessoaFisicaComMesmoCpf.Cpf.Numero).Wait();
			};

			act.Should().Throw<PessoaFisicaCpfJaExistenteException>();


		}
	}
}
