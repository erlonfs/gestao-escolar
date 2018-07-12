using Microsoft.EntityFrameworkCore;
using SharedKernel.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Alunos.Infra.EF.Repositories
{
	public class Repository<TEntity> where TEntity : Entity<Guid>
	{
		private DbSet<TEntity> _dbSet;
		private DbContext _context;

		protected Repository(DbContext context)
		{
			_context = context;
			_dbSet = context.Set<TEntity>();
		}

		public async Task AddAsync(TEntity entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async Task<TEntity> GetByEntityIdAsync(Guid entityId)
		{
			var result = _dbSet.Local.SingleOrDefault(x => x.EntityId == entityId);
			if(result == null)
			{
				result = await _dbSet.SingleOrDefaultAsync(x => x.EntityId == entityId);
			}

			return result;

		}

		public Task RemoveAsync(TEntity entity)
		{
			_dbSet.Remove(entity);

			return Task.CompletedTask;

		}
	}
}
