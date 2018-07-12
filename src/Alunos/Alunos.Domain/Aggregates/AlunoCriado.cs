using SharedKernel.Common;
using System;

namespace Alunos.Domain.Aggregates
{
	public class AlunoCriado : IDomainEvent
	{
		public Guid AggregateId { get; }
		public Aluno Aluno { get; }
		public DateTime DataExecucao { get; }

		public AlunoCriado(Guid aggregateId, Aluno aluno)
		{
			AggregateId = aggregateId;
			Aluno = aluno;
			DataExecucao = DateTime.Now;
		}
	}
}
