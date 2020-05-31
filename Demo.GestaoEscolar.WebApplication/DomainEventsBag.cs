using Autofac;
using CrossCutting;
using System;
using System.Collections.Generic;

namespace Demo.GestaoEscolar.WebApplication
{
	public class DomainEventsBag : IDomainEventsBag
	{
		////[ThreadStatic]
		//private static List<Delegate> _actions;

		//private List<IDomainEvent> _events;

		//public DomainEventsBag()
		//{

		//}

		//public void Init(ILifetimeScope scope)
		//{
		//	_scope = scope;
		//	_actions = new List<Delegate>();
		//	_events = new List<IDomainEvent>();
		//}

		//static ILifetimeScope _scope { get; set; }

		//public void Register(Action<IDomainEvent> callback)
		//{
		//	if (_actions == null) { _actions = new List<Delegate>(); }

		//	_actions.Add(callback);
		//}

		//public void ClearCallbacks()
		//{
		//	_actions.Clear();
		//}

		//public void Raise(IDomainEvent args)
		//{
		//	if (_events == null) { _events = new List<IDomainEvent>(); }

		//	_events.Add(args);

		//	if (_scope != null)
		//	{
		//		foreach (var handler in _scope.ResolveOptional<IEnumerable<IHandler<IDomainEvent>>>())
		//		{
		//			try
		//			{
		//				handler.HandleAsync(args).ConfigureAwait(true).GetAwaiter().GetResult();
		//			}
		//			catch (Exception)
		//			{
		//				throw;
		//			}

		//		}
		//	}

		//	if (_actions != null)
		//	{
		//		foreach (var action in _actions)
		//		{
		//			if (action is Action<IDomainEvent>) { ((Action<IDomainEvent>)action)(args); }
		//		}
		//	}
		//}

		//public IReadOnlyList<IDomainEvent> GetEvents()
		//{
		//	if (_events == null) return new List<IDomainEvent>().AsReadOnly();
		//	return _events.AsReadOnly();
		//}

		//~DomainEventsBag()
		//{
		//	_events.Clear();
		//}


		public DomainEventsBag()
		{

		}
		public IReadOnlyList<IDomainEvent> GetEvents()
		{
			throw new NotImplementedException();
		}

		public void Raise(IDomainEvent args)
		{
			throw new NotImplementedException();
		}

		public void Register(Action<IDomainEvent> callback)
		{
			throw new NotImplementedException();
		}
	}
}
