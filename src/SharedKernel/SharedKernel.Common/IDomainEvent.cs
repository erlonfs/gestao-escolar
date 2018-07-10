using System;

namespace SharedKernel.Common
{
	public interface IDomainEvent
	{
		Guid AggregateId { get; }
		DateTime DataCriacao { get; }
	}
}
