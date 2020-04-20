using System;
using System.Threading.Tasks;
using CrossCutting;
using MassTransit;
using NLog;

namespace Demo.GestaoEscolar.WebApplication.Test
{
	public class MessageBusFake : IMessageBus
	{
		public Task<bool> IsAliveAsync()
		{
			return Task.FromResult(true);
		}

		public Task PublishAsync<T>(T e)
		{
			return Task.CompletedTask;
		}
	}
}