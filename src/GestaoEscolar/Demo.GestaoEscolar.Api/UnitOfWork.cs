using CrossCutting;
using Demo.GestaoEscolar.Infra.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Api
{
    public class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext _dbContext;
		private readonly IMessageBus _messageBus;

		public UnitOfWork(AppDbContext dbContext,
						 IMessageBus messageBus)
		{
			_dbContext = dbContext;
			_messageBus = messageBus;
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

						if(!await _messageBus.IsAliveAsync())
						{
							throw new ServiceMessageBusUnavailableException();
						}

						transaction.Commit();

						foreach (var e in DomainEvents.GetEvents())
						{
							await _messageBus.PublishAsync(e);
						}

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
