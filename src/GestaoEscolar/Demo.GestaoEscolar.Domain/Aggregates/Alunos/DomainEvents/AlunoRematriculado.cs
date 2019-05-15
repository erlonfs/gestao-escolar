using CrossCutting;
using System;

namespace Demo.GestaoEscolar.Domain.Aggregates.Alunos
{
    public class AlunoRematriculado : IDomainEvent
	{
		public Guid AggregateId { get; }
		public Aluno Aluno { get; }
		public DateTime DataExecucao { get; }

		public AlunoRematriculado(Guid aggregateId, Aluno aluno)
		{
			AggregateId = aggregateId;
			Aluno = aluno;
			DataExecucao = DateTime.Now;
		}
	}
}
