using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Demo.GestaoEscolar.Infra.MongoDb
{
    public interface IMongoContext : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
