using Microsoft.EntityFrameworkCore;
using PessoasFisicas.Infra.EF;
using SharedKernel.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PessoasFisicas.Api
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly PessoasFisicasDbContext _appContext;

		public UnitOfWork(PessoasFisicasDbContext appContext)
		{
			_appContext = appContext;
		}

		public async Task CommitAsync()
		{
			try
			{
				using (var transaction = await _appContext.Database.BeginTransactionAsync())
				{
					try
					{
						await _appContext.SaveChangesAsync();

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
				var changedEntries = _appContext.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

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
