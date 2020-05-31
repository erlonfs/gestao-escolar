using System;
using System.Collections.Generic;

namespace CrossCutting
{
	public interface IDomainEventsBag
	{
		void Register(Action<IDomainEvent> callback);
		void Raise(IDomainEvent args);
		IReadOnlyList<IDomainEvent> GetEvents();
	}
}
