using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.Dapper.Data.Alunos
{
	public interface IAlunoFinder
	{
		Task<IEnumerable<AlunoDto>> ObterAsync();
	}
}
