using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrossCutting;
using MongoDB.Driver;

namespace Demo.GestaoEscolar.Infra.MongoDb.Repositories
{
	public class Repository<TEntity> where TEntity : Entity<Guid>
	{
		protected readonly IMongoContext _context;
		protected readonly IMongoCollection<TEntity> DbSet;

		protected Repository(IMongoContext context)
		{
			_context = context;
			DbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
		}


		public async Task AddAsync(TEntity entity)
		{
			await DbSet.InsertOneAsync(entity);
		}

		public async Task UpdateAsync(TEntity entity)
		{
			var filter = Builders<TEntity>.Filter.Eq("EntityId", entity.EntityId);

			await DbSet.ReplaceOneAsync(filter, entity);
		}

		public async Task<TEntity> GetByEntityIdAsync(Guid entityId)
		{
			var filter = Builders<TEntity>.Filter.Eq("EntityId", entityId);

			var result = await DbSet.FindAsync<TEntity>(filter);

			return await result.SingleOrDefaultAsync();

		}

		public async Task RemoveAsync(TEntity entity)
		{
			var filter = Builders<TEntity>.Filter.Eq("EntityId", entity.EntityId);

			await DbSet.DeleteOneAsync(filter);

		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			var filter = Builders<TEntity>.Filter.Eq("entityId", Guid.NewGuid());

			var result = await DbSet.FindAsync<TEntity>(filter);

			return result.ToEnumerable();
		}
	}
}
