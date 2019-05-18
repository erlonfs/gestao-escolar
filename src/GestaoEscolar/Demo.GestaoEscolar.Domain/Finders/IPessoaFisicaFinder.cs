using Demo.GestaoEscolar.Domain.Finders.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Domain.Finders
{
	public interface IPessoaFisicaFinder
	{
		Task<IEnumerable<PessoaFisicaDto>> ObterAsync();
	}
}
