using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Demo.GestaoEscolar.Domain.Test.Doubles;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Demo.GestaoEscolar.Domain.Test.Aggregates
{
	public class EscolaTest
	{
		private Escola _aggregate;

		private readonly Guid _escolaId = Guid.NewGuid();
		private readonly string _nome = "Escola de Testes";

		private readonly Guid _salaId = Guid.NewGuid();
		private readonly string _faseAno = "1º ANO";
		private readonly Turno _turnoMatutino = Turno.Matutino;

		private readonly Aluno _aluno = new Aluno(Guid.NewGuid(), PessoaFisicaStub.PessoaMenorDeIdade, PessoaFisicaStub.PessoaMaiorDeIdade, 4878);

		public EscolaTest()
		{
			
		}

		[Fact]
		public void criar_escola__com_todos_os_parametros__deve_atribuir_os_valores()
		{
			_aggregate = new Escola(_escolaId, _nome);

			_aggregate.EntityId.Should().Be(_escolaId);
			_aggregate.DataCriacao.Date.Should().Be(DateTime.Today);
			_aggregate.Nome.Should().Be(_nome);
		}

		[Fact]
		public void adicionar_sala_na_escola__com_todos_os_parametros__deve_atribuir_os_valores()
		{
			_aggregate = new Escola(_escolaId, _nome);

			_aggregate.AdicionarSala(_salaId, _faseAno, _turnoMatutino);

			_aggregate.EntityId.Should().Be(_escolaId);
			_aggregate.Nome.Should().Be(_nome);

			var sala = _aggregate.Salas.SingleOrDefault(x => x.EntityId == _salaId);

			sala.EntityId.Should().Be(_salaId);
			sala.FaseAno.Should().Be(_faseAno);
			sala.TurnoId.Should().Be((int)_turnoMatutino);
		}

		[Fact]
		public void adicionar_aluno_na_sala__com_todos_os_parametros__deve_atribuir_os_valores()
		{
			_aggregate = new Escola(_escolaId, _nome);

			_aggregate.AdicionarSala(_salaId, _faseAno, _turnoMatutino);

			_aggregate.EntityId.Should().Be(_escolaId);
			_aggregate.Nome.Should().Be(_nome);

			var sala = _aggregate.Salas.SingleOrDefault(x => x.EntityId == _salaId);

			sala.EntityId.Should().Be(_salaId);
			sala.FaseAno.Should().Be(_faseAno);
			sala.TurnoId.Should().Be((int)_turnoMatutino);

			_aggregate.AdicionarAluno(_salaId, _aluno);

			var alunoSala = sala.Alunos.SingleOrDefault(x => x.Aluno.EntityId == _aluno.EntityId);

			alunoSala.Aluno.EntityId.Should().Be(_aluno.EntityId);
			alunoSala.Aluno.PessoaFisica.EntityId.Should().Be(_aluno.PessoaFisica.EntityId);
			alunoSala.Aluno.Responsavel.EntityId.Should().Be(_aluno.Responsavel.EntityId);
		}
	}
}
