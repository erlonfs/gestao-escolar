using CrossCutting;
using NLog;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Api
{
    public class MessageBus : IMessageBus
	{
		private Logger _logger = LogManager.GetCurrentClassLogger();

		public Task<bool> IsAliveAsync()
		{
			//Verificar disponibilidade do serviço
			return Task.FromResult(true);
		}

		public Task PublishAsync<T>(T e)
		{
			_logger.Info(e.GetType().ToString(), e);

			return Task.CompletedTask;
		}
	}
}
