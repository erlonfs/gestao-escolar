using PessoasFisicas.Domain.Aggregates;

namespace PessoasFisicas.Domain.Repositories
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
