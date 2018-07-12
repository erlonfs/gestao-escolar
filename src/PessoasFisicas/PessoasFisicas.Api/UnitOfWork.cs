using Demo.GerenciamentoEscolar.Infra.EF;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.GerenciamentoEscolar.Api
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext _dbContext;

		public UnitOfWork(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task CommitAsync()
		{
			try
			{
				using (var transaction = await _dbContext.Database.BeginTransactionAsync())
				{
					try
					{
						await _dbContext.SaveChangesAsync();

						transaction.Commit();

					}
					catch (Exception)
					{
						transaction.Rollback();
						throw;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public Task RollBackAsync()
		{
			try
			{
				var changedEntries = _dbContext.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

				foreach (var entry in changedEntries)
				{
					switch (entry.State)
					{
						case EntityState.Modified:
							entry.CurrentValues.SetValues(entry.OriginalValues);
							entry.State = EntityState.Unchanged;
							break;
						case EntityState.Added:
							entry.State = EntityState.Detached;
							break;
						case EntityState.Deleted:
							entry.State = EntityState.Unchanged;
							break;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return Task.CompletedTask;

		}
	}
}
