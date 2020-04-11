namespace CrossCutting
{
	public class Aggregate<TId> : Entity<TId>
	{
		protected void RaiseEvent<T>(T domainEvent) where T : IDomainEvent
		{
			DomainEvents.Raise(domainEvent);
		}

	}
}