using Demo.GestaoEscolar.Infra.Data.PessoaFisica;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.Data
{
	public interface IPessoaFisicaFinder
	{
		Task<PessoaFisicaDto> ObterAsync();
	}
}
