using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Demo.GestaoEscolar.Domain.Repositories.Escolas;

namespace Demo.GestaoEscolar.Infra.EF.Repositories.Escolas
{
	public class EscolaRepository : Repository<Escola>,  IEscolaRepository
	{
		private AppDbContext _context;

		public EscolaRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
