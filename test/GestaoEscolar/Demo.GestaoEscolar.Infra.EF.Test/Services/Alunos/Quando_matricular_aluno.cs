using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Repositories.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Services.Alunos;
using Demo.GestaoEscolar.Infra.EF.Services.Alunos;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Demo.GestaoEscolar.Infra.EF.Test.Services.Alunos
{
	public class Quando_matricular_aluno
	{
		private Aluno _aluno;
		private readonly Guid _alunoId = Guid.NewGuid();
		private readonly int _matricula = 7802;

		private PessoaFisica _pessoaFisica;
		private readonly Guid _pessoaFisicaId = Guid.NewGuid();
		private readonly string _nome = "Erlon F Souza";
		private readonly string _cpf = "03443703135";
		private readonly string _nomeSocial = null;
		private readonly string _sexo = "M";
		private readonly DateTime _dataNascimento = new DateTime(1990, 04, 02);

		private AlunoService _service;

		public Quando_matricular_aluno()
		{
			_pessoaFisica = new PessoaFisica(_pessoaFisicaId, _nome, _cpf, _nomeSocial, _sexo, _dataNascimento);

			var mockAlunoRepository = new Mock<IAlunoRepository>();
			var mockPessoaFisicaRepository = new Mock<IPessoaFisicaRepository>();
			var mockMatriculaService = new Mock<IMatriculaService>();

			mockAlunoRepository.Setup(x => x.AddAsync(It.IsAny<Aluno>()))
				.Callback((Aluno a) => { _aluno = a; });

			mockPessoaFisicaRepository.Setup(x => x.GetByEntityIdAsync(It.IsAny<Guid>()))
				.Returns(Task.FromResult(_pessoaFisica));

			mockMatriculaService.Setup(x => x.GerarMatriculaAsync())
				.Returns(Task.FromResult(_matricula));

			_service = new AlunoService(mockAlunoRepository.Object,
										mockPessoaFisicaRepository.Object,
										mockMatriculaService.Object);

			CallSync(() => _service.MatricularAsync(_alunoId, _pessoaFisicaId).Wait());

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
			_aluno.Situacao.Should().Be(AlunoSituacao.Matriculado);
		}

		protected static void CallSync(Action target)
		{
			var task = new Task(target);
			task.RunSynchronously();
		}
	}
}
