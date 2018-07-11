using PessoasFisicas.Domain.Aggregates;
using PessoasFisicas.Domain.Repositories;

namespace PessoasFisicas.Infra.EF.Repositories
{
	public class PessoaFisicaRepository : Repository<PessoaFisica>,  IPessoaFisicaRepository
	{
		private PessoasFisicasDbContext _context;

		public PessoaFisicaRepository(PessoasFisicasDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
