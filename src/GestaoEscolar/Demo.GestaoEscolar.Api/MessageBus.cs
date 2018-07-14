using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;

namespace Demo.GestaoEscolar.Api
{
	public class MessageBus : IMessageBus
	{
		private readonly ILogger<MessageBus> _logger;

		public MessageBus()
		{
			
		}

		public Task PublishAsync<T>(T e)
		{
			return Task.CompletedTask;
		}
	}
}
