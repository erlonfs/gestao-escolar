﻿using CrossCutting;
using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Repositories.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.Escolas;
using Demo.GestaoEscolar.Domain.Repositories.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Services.Alunos;
using Demo.GestaoEscolar.Domain.Test.Doubles.PessoasFisicas;
using Demo.GestaoEscolar.Infra.EF.Services.Alunos;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Demo.GestaoEscolar.Infra.EF.Test.Services.Alunos
{
	public class Quando_transferir_aluno
	{
		private Aluno _aluno;
		private readonly Guid _alunoId = Guid.NewGuid();
		private readonly int _matricula = 7802;

		private Escola _escola;
		private readonly Guid _escolaId = Guid.NewGuid();
		private readonly string _escolaNome = "Escola Estadual Ana Maria do Couto";

		private readonly Guid _salaId = Guid.NewGuid();
		private readonly string _salaFaseAno = "8ºANO";

		private PessoaFisica _pessoaFisica;
		private PessoaFisica _responsavel;

		private AlunoService _service;

		public Quando_transferir_aluno()
		{
			_pessoaFisica = PessoaFisicaStub.PessoaMenorDeIdade;
			_responsavel = PessoaFisicaStub.PessoaMaiorDeIdade;

			_escola = new Escola(_escolaId, _escolaNome);
			_escola.AdicionarSala(_salaId, _salaFaseAno, Turno.Matutino);

			_aluno = new Aluno(_alunoId, _pessoaFisica, _responsavel, 1334);

			var mockAlunoRepository = new Mock<IAlunoRepository>();
			var mockPessoaFisicaRepository = new Mock<IPessoaFisicaRepository>();
			var mockEscolaRepository = new Mock<IEscolaRepository>();
			var mockMatriculaService = new Mock<IMatriculaService>();

			mockAlunoRepository.Setup(x => x.GetByEntityIdAsync(It.IsAny<Guid>()))
				.Returns(Task.FromResult(_aluno));

			mockPessoaFisicaRepository.Setup(x => x.GetByEntityIdAsync(It.IsAny<Guid>()))
				.Returns((Guid entityId) =>
					{
						if (entityId == _pessoaFisica.EntityId) return Task.FromResult(_pessoaFisica);
						if (entityId == _responsavel.EntityId) return Task.FromResult(_responsavel);

						return Task.FromResult<PessoaFisica>(null);

					});

			mockEscolaRepository.Setup(x => x.GetByEntityIdAsync(It.IsAny<Guid>()))
				.Returns(Task.FromResult(_escola));

			mockEscolaRepository.Setup(x => x.ObterPorAlunoIdAsync(It.IsAny<Guid>()))
				.Returns((Guid entityId) =>
				{
					if (entityId == _alunoId) return Task.FromResult(_escola);

					return Task.FromResult<Escola>(null);

				});

			mockEscolaRepository.Setup(x => x.RemoverAlunoPorAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns((Guid escola, Guid alunoId) =>
			{
				var sala = _escola.Salas.SingleOrDefault(x => x.Alunos.Any(y => y.Aluno.EntityId == alunoId));

				sala.Alunos.Remove(sala.Alunos.SingleOrDefault(x => x.Aluno.EntityId == alunoId));

				return Task.CompletedTask;
			});

			mockMatriculaService.Setup(x => x.GerarMatriculaAsync())
				.Returns(Task.FromResult(_matricula));

			_service = new AlunoService(mockAlunoRepository.Object,
										mockPessoaFisicaRepository.Object,
										mockEscolaRepository.Object,
										mockMatriculaService.Object);

			TestAsyncHelper.CallSync(() => _service.MatricularAsync(_alunoId, _pessoaFisica.EntityId, _responsavel.EntityId, _escolaId, _salaId).Wait());
			TestAsyncHelper.CallSync(() => _service.TransferirAsync(_alunoId).Wait());

		}

		[Fact]
		public void Quando_transferir_aluno_devera_constar_situacao_transferido()
		{
			_aluno.SituacaoId.Should().Be((int)AlunoSituacao.Transferido);
		}

		[Fact]
		public void Quando_transferir_aluno_nao_devera_constar_na_escola_antiga()
		{
			var sala = _escola.Salas.SingleOrDefault(x => x.EntityId == _salaId);
			sala.Alunos.Select(x => x.Aluno).Should().NotContain(_aluno);
		}
	}
}
