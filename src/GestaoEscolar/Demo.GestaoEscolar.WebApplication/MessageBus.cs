using System;
using System.Threading.Tasks;
using CrossCutting;
using MassTransit;
using NLog;

namespace Demo.GestaoEscolar.WebApplication
{
	public class MessageBus : IMessageBus
	{
		private Logger _logger = LogManager.GetCurrentClassLogger();

		public Task<bool> IsAliveAsync()
		{
			//Verificar disponibilidade do serviço
			return Task.FromResult(true);
		}

		public async Task PublishAsync<T>(T e)
		{

			var busControl = Bus.Factory.CreateUsingRabbitMq(cfg => cfg.Host("rabbitmq://10.0.75.1"));
			await busControl.StartAsync();

			try
			{
				await busControl.Publish(e);
			}
			catch (Exception ex)
			{
				_logger.Info(e.GetType().ToString(), $"Ocorreu um erro ao processar o evento {e}. Erro {ex.Message} InnerException {ex.InnerException}");
			}
			finally
			{
				await busControl.StopAsync();
			}

		}
	}
}