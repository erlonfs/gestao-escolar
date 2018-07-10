using System;

namespace SharedKernel.Common
{
	public class Entity<TId> : IEquatable<Entity<TId>>
	{
		private TId _entityId;

		protected Entity(TId entityId)
		{
			if (Equals(entityId, default(TId)))
			{
				throw new ArgumentException("The ID cannot be the default value.", "id");
			}

			_entityId = entityId;
		}

		protected Entity()
		{

		}

		public TId EntityId
		{
			get { return _entityId; }
			set { _entityId = value; }
		}

		public override bool Equals(object obj)
		{
			var entity = obj as Entity<TId>;
			if (entity != null)
			{
				return Equals(entity);
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return EntityId.GetHashCode();
		}

		public bool Equals(Entity<TId> other)
		{
			if (other == null)
			{
				return false;
			}
			return EntityId.Equals(other.EntityId);
		}
	}
}