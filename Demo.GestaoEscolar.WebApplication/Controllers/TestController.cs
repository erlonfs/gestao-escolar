﻿using CrossCutting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.WebApplication.Controllers
{
	[Produces("application/json")]
	[Route("api/test")]
	public class TestController : BaseApiController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMessageBus _messageBus;

		public TestController(IUnitOfWork unitOfWork, IMessageBus messageBus)
		{
			_unitOfWork = unitOfWork;
			_messageBus = messageBus;
		}

		[HttpPost]
		[Route("/bulk-messages")]
		public Task BulkMessagesAsync(int numberOfMessages)
		{
			for (int i = 0; i < numberOfMessages; i++)
			{
				Task.Run(() =>
			   {
				   _messageBus.PublishAsync(new Message
				   {
					   Text = $"{Guid.NewGuid()} > Mensagem de teste {DateTime.Now}"
				   });
			   });
			}

			return Task.CompletedTask;

		}

		[HttpPost]
		[Route("/bulk-messages-with-await")]
		public async Task BulkMessagesWithAwaitAsync(int numberOfMessages)
		{
			for (int i = 0; i < numberOfMessages; i++)
			{
				await _messageBus.PublishAsync(new Message
				{
					Text = $"{Guid.NewGuid()} > Mensagem de teste {DateTime.Now}"
				});

			}

		}
	}


	public class Message
	{
		public string Text { get; set; }
	}

}
