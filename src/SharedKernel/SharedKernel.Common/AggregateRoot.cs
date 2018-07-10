using System;
using System.Collections.Generic;

namespace SharedKernel.Common
{
	public abstract class AggregateRoot : Entity<Guid>
	{
		private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
		public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

		protected AggregateRoot() : base(new Guid())
		{

		}

		protected virtual void AddDomainEvent(IDomainEvent newEvent)
		{
			_domainEvents.Add(newEvent);
		}

		public virtual void ClearEvents()
		{
			_domainEvents.Clear();
		}
	}
}
