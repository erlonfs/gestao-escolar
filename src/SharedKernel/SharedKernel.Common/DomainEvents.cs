using Autofac;
using System;
using System.Collections.Generic;

namespace SharedKernel.Common
{
	public class DomainEvents
	{
		[ThreadStatic]
		private static List<Delegate> _actions;

		private static List<IDomainEvent> _events;

		public static void Init(ILifetimeScope scope)
		{
			_scope = scope;
			_actions = new List<Delegate>();
			_events = new List<IDomainEvent>();
		}

		static ILifetimeScope _scope { get; set; }

		public static void Register<T>(Action<T> callback) where T : IDomainEvent
		{
			if (_actions == null) { _actions = new List<Delegate>(); }

			_actions.Add(callback);
		}

		public void ClearCallbacks()
		{
			_actions.Clear();
		}

		public void ClearEvents()
		{
			_events.Clear();
		}

		public static void Raise<T>(T args) where T : IDomainEvent
		{
			if (_events == null) { _events = new List<IDomainEvent>(); }

			_events.Add(args);

			if (_scope != null)
			{
				foreach (var handler in _scope.ResolveOptional<IEnumerable<IHandler<T>>>())
				{
					handler.HandleAsync(args).ConfigureAwait(true);
				}
			}

			if (_actions != null)
			{
				foreach (var action in _actions)
				{
					if (action is Action<T>) { ((Action<T>)action)(args); }
				}
			}
		}

		public static IReadOnlyList<IDomainEvent> GetEvents()
		{
			if (_events == null) return new List<IDomainEvent>().AsReadOnly();
			return _events.AsReadOnly();
		}
	}
}
