using CrossCutting;
using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using System;

namespace Demo.GestaoEscolar.Agregates.Alunos
{
    public class AlunoMatriculado : IDomainEvent
	{
		public Guid AggregateId { get; }
		public Aluno Aluno { get; }
		public DateTime DataExecucao { get; }

		public AlunoMatriculado(Guid aggregateId, Aluno aluno)
		{
			AggregateId = aggregateId;
			Aluno = aluno;
			DataExecucao = DateTime.Now;
		}
	}
}
