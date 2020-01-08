using System;
using System.Threading.Tasks;
using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Demo.GestaoEscolar.Domain.Repositories.Escolas;
using MongoDB.Driver;

namespace Demo.GestaoEscolar.Infra.MongoDb.Repositories.Escolas
{
	public class EscolaRepository : Repository<Escola>,  IEscolaRepository
	{
		public EscolaRepository(IMongoContext context) : base(context)
		{
			
		}

		public Task<Escola> ObterPorAlunoIdAsync(Guid alunoId)
		{
			throw new NotImplementedException();
		}

		public Task RemoverAlunoPorAsync(Guid escolaId, Guid alunoId)
		{
			throw new NotImplementedException();

		}
	}
}
