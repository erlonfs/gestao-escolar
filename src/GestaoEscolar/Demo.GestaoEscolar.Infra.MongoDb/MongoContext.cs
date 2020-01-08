using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Demo.GestaoEscolar.Infra.MongoDb
{
	public class MongoContext : IMongoContext
	{
		private IMongoDatabase Database { get; set; }
		private readonly List<Func<Task>> _commands;

		public IClientSessionHandle Session { get; set; }
		public MongoClient MongoClient { get; set; }


		public MongoContext(IConfiguration configuration)
		{
			BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

			_commands = new List<Func<Task>>();

			RegisterConventions();

			MongoClient = new MongoClient("mongodb://10.0.75.1:27017");
			Database = MongoClient.GetDatabase("gestaoescolar");
		}

		private void RegisterConventions()
		{
			var pack = new ConventionPack
		{
			new IgnoreExtraElementsConvention(true),
			new IgnoreIfDefaultConvention(true)

		};
			ConventionRegistry.Register("My Solution Conventions", pack, t => true);
		}

		public async Task<int> SaveChanges()
		{
			using (Session = await MongoClient.StartSessionAsync())
			{
				Session.StartTransaction();

				var commandTasks = _commands.Select(c => c());

				await Task.WhenAll(commandTasks);

				await Session.CommitTransactionAsync();
			}

			return _commands.Count;
		}

		public IMongoCollection<T> GetCollection<T>(string name)
		{
			return Database.GetCollection<T>(name);
		}

		public void Dispose()
		{
			while (Session != null && Session.IsInTransaction)
				Thread.Sleep(TimeSpan.FromMilliseconds(100));

			GC.SuppressFinalize(this);
		}

		public void AddCommand(Func<Task> func)
		{
			_commands.Add(func);
		}
	}
}
