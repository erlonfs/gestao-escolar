using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.Dapper.Data.PessoasFisicas
{
	public interface IPessoaFisicaFinder
	{
		Task<IEnumerable<PessoaFisicaDto>> ObterAsync();
	}
}
