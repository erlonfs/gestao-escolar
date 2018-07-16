using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.Data
{
	public interface IAlunoFinder
	{
		Task<IEnumerable<AlunoDto>> ObterAsync();
	}
}
