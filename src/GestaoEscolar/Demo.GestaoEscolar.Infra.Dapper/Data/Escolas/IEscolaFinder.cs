using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.Dapper.Data.Escolas
{
	public interface IEscolaFinder
	{
		Task<IEnumerable<EscolaDto>> ObterAsync();
	}
}
