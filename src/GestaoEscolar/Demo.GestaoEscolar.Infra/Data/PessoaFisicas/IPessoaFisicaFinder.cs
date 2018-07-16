using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.Data
{
	public interface IPessoaFisicaFinder
	{
		Task<IEnumerable<PessoaFisicaDto>> ObterAsync();
	}
}
