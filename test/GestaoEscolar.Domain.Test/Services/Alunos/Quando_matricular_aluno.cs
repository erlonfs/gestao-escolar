using CrossCutting;
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

namespace Demo.GestaoEscolar.Domain.Test.Services.Alunos
{
	public class Quando_matricular_aluno
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

		public Quando_matricular_aluno()
		{
			_pessoaFisica = PessoaFisicaStub.PessoaMenorDeIdade;
			_responsavel = PessoaFisicaStub.PessoaMaiorDeIdade;

			_escola = new Escola(_escolaId, _escolaNome);
			_escola.AdicionarSala(_salaId, _salaFaseAno, Turno.Matutino);

			var mockAlunoRepository = new Mock<IAlunoRepository>();
			var mockPessoaFisicaRepository = new Mock<IPessoaFisicaRepository>();
			var mockEscolaRepository = new Mock<IEscolaRepository>();
			var mockMatriculaService = new Mock<IMatriculaService>();

			mockAlunoRepository.Setup(x => x.AddAsync(It.IsAny<Aluno>()))
				.Callback((Aluno a) => { _aluno = a; });

			mockPessoaFisicaRepository.Setup(x => x.GetByEntityIdAsync(It.IsAny<Guid>()))
				.Returns((Guid entityId) =>
					{
						if (entityId == _pessoaFisica.EntityId) return Task.FromResult(_pessoaFisica);
						if (entityId == _responsavel.EntityId) return Task.FromResult(_responsavel);

						return Task.FromResult<PessoaFisica>(null);

					});

			mockEscolaRepository.Setup(x => x.GetByEntityIdAsync(It.IsAny<Guid>()))
				.Returns(Task.FromResult(_escola));

			mockMatriculaService.Setup(x => x.GerarMatriculaAsync())
				.Returns(Task.FromResult(_matricula));

			_service = new AlunoService(mockAlunoRepository.Object,
										mockPessoaFisicaRepository.Object,
										mockEscolaRepository.Object,
										mockMatriculaService.Object);

			TestAsyncHelper.CallSync(() => _service.MatricularAsync(_alunoId, _pessoaFisica.EntityId, _responsavel.EntityId,  _escolaId, _salaId).Wait());

		}

		[Fact]
		public void Quando_matricular_aluno_devera_constar_pessoaFisica()
		{
			_aluno.PessoaFisica.Should().Be(_pessoaFisica);
		}

		[Fact]
		public void Quando_matricular_aluno_devera_constar_matricula()
		{
			_aluno.Matricula.Should().Be(_matricula);
		}

		[Fact]
		public void Quando_matricular_aluno_devera_constar_situacao_matriculado()
		{
			_aluno.SituacaoId.Should().Be((int)AlunoSituacao.Matriculado);
		}

		[Fact]
		public void Quando_matricular_aluno_devera_constar_na_escola_e_sala()
		{
			var sala = _escola.Salas.SingleOrDefault(x => x.EntityId == _salaId);
			sala.Alunos.Select(x => x.Aluno).Should().Contain(_aluno);
		}
	}
}
