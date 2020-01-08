using System;
using System.Threading.Tasks;
using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Repositories.PessoasFisicas;
using MongoDB.Driver;

namespace Demo.GestaoEscolar.Infra.MongoDb.Repositories.PessoasFisicas
{
	public class PessoaFisicaRepository : Repository<PessoaFisica>, IPessoaFisicaRepository
	{
		public PessoaFisicaRepository(IMongoContext context) : base(context)
		{

		}

		public async Task<PessoaFisica> ObterPorCpfAsync(string cpf)
		{
			var filter = Builders<PessoaFisica>.Filter.Eq("Cpf.Numero", cpf);

			var result = await DbSet.FindAsync<PessoaFisica>(filter);

			return await result.FirstOrDefaultAsync();
		}
	}
}
