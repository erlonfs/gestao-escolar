using System.Linq;
using System.Threading.Tasks;
using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Repositories.PessoasFisicas;
using Microsoft.EntityFrameworkCore;

namespace Demo.GestaoEscolar.Infra.EF.Repositories.PessoasFisicas
{
	public class PessoaFisicaRepository : Repository<PessoaFisica>,  IPessoaFisicaRepository
	{
		private AppDbContext _context;

		public PessoaFisicaRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<PessoaFisica> ObterPorCpfAsync(string cpf)
		{
			return await _context.PessoaFisica.FirstOrDefaultAsync(x => x.Cpf.Numero == cpf);
		}
	}
}
