using Demo.GerenciamentoEscolar.Infra.Data.PessoaFisica;
using System.Threading.Tasks;

namespace Demo.GerenciamentoEscolar.Infra.Data
{
	public interface IPessoaFisicaFinder
	{
		Task<PessoaFisicaDto> ObterAsync();
	}
}
