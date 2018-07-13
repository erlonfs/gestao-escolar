using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Repositories.PessoasFisicas;

namespace Demo.GestaoEscolar.Infra.EF.Repositories
{
	public class PessoaFisicaRepository : Repository<PessoaFisica>,  IPessoaFisicaRepository
	{
		private AppDbContext _context;

		public PessoaFisicaRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
