using System;

namespace CrossCutting
{
	public interface IDomainEvent
	{
		Guid AggregateId { get; }
		DateTime DataExecucao { get; }
	}
}
