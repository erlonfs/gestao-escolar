using Demo.GestaoEscolar.Agregates.Alunos;
using Demo.GestaoEscolar.Domain.Test.Doubles;
using FluentAssertions;
using System;
using Xunit;

namespace Demo.GestaoEscolar.Domain.Test.DomainEvents
{
	public class AlunoDomainEventsTest
	{
		[Fact]
		public void aluno_matriculado_deve_constar_todos_os_valores_atribuidos()
		{
			var aluno = AlunoStub.AlunoValido;
			var e = new AlunoMatriculado(aluno.EntityId, aluno);

			e.AggregateId.Should().Be(aluno.EntityId);
			e.Aluno.EntityId.Should().Be(aluno.EntityId);
			e.DataExecucao.Date.Should().Be(DateTime.Today);
		}


		[Fact]
		public void aluno_rematriculado_deve_constar_todos_os_valores_atribuidos()
		{
			var aluno = AlunoStub.AlunoValido;
			var e = new AlunoRematriculado(aluno.EntityId, aluno);

			e.AggregateId.Should().Be(aluno.EntityId);
			e.Aluno.EntityId.Should().Be(aluno.EntityId);
			e.DataExecucao.Date.Should().Be(DateTime.Today);
		}


		[Fact]
		public void aluno_transferido_deve_constar_todos_os_valores_atribuidos()
		{
			var aluno = AlunoStub.AlunoValido;
			var e = new AlunoTransferido(aluno.EntityId, aluno);

			e.AggregateId.Should().Be(aluno.EntityId);
			e.Aluno.EntityId.Should().Be(aluno.EntityId);
			e.DataExecucao.Date.Should().Be(DateTime.Today);
		}
	}
}
