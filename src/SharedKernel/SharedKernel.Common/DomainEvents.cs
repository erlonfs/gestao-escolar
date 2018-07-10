using Autofac;
using System;
using System.Collections.Generic;

namespace SharedKernel.Common
{
	public class DomainEvents
	{
		[ThreadStatic]
		private static List<Delegate> actions;

		public static void Init(ILifetimeScope scope)
		{
			_scope = scope;
		}

		static ILifetimeScope _scope { get; set; }

		public static void Register<T>(Action<T> callback) where T : IDomainEvent
		{
			if (actions == null) { actions = new List<Delegate>(); }
			actions.Add(callback);
		}

		public void ClearCallbacks()
		{
			actions = null;
		}

		public static void Raise<T>(T args) where T : IDomainEvent
		{
			if (_scope != null)
			{
				foreach (var handler in _scope.ResolveOptional<IEnumerable<IHandler<T>>>())
				{
					handler.HandleAsync(args).ConfigureAwait(true);
				}
			}

			if (actions != null)
			{
				foreach (var action in actions)
				{
					if (action is Action<T>) { ((Action<T>)action)(args); }
				}
			}
		}
	}
}
