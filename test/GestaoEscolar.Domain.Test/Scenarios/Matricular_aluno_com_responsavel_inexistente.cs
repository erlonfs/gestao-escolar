using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Exceptions.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.Escolas;
using Demo.GestaoEscolar.Domain.Repositories.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Services.Alunos;
using Demo.GestaoEscolar.Domain.Test.Doubles;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Demo.GestaoEscolar.Domain.Test.Scenarios
{
	public class Matricular_aluno_com_responsavel_inexistente
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

		public Matricular_aluno_com_responsavel_inexistente()
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
		}

		[Fact]
		public void Deve_lancar_exception()
		{
			Action act = () =>
			{
				_service.MatricularAsync(_alunoId, _pessoaFisica.EntityId, _responsavel.EntityId, _escolaId, _salaId).Wait();
			};

			act.Should().Throw<ResponsavelNaoEncontradoException>();

		}
	}
}
