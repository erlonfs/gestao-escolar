using System;
using System.Threading.Tasks;
using CrossCutting;

namespace Demo.GestaoEscolar.Api
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IMessageBus _messageBus;

		public UnitOfWork(IMessageBus messageBus)
		{
			_messageBus = messageBus;
		}

		public async Task CommitAsync()
		{
			try
			{
				try
				{
					//await _dbContext.SaveChangesAsync();

					if (!await _messageBus.IsAliveAsync())
					{
						throw new ServiceMessageBusUnavailableException();
					}

					//transaction.Commit();

					foreach (var e in DomainEvents.GetEvents())
					{
						await _messageBus.PublishAsync(e);
					}

					DomainEvents.ClearEvents();

				}
				catch (Exception)
				{
					//transaction.Rollback();
					throw;
				}

			}
			catch (Exception)
			{
				throw;
			}
		}

		public Task RollBackAsync()
		{
			return Task.CompletedTask;
		}
	}
}
