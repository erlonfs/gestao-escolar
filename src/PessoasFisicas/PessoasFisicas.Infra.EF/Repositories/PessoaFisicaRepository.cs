using Demo.GerenciamentoEscolar.Domain.Aggregates.PessoasFisicas;
using Demo.GerenciamentoEscolar.Domain.Repositories.PessoasFisicas;

namespace Demo.GerenciamentoEscolar.Infra.EF.Repositories
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
