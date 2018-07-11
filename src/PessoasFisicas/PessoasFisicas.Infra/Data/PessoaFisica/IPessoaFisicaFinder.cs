using PessoasFisicas.Infra.Data.PessoaFisica;
using System.Threading.Tasks;

namespace PessoasFisicas.Infra.Data
{
	public interface IPessoaFisicaFinder
	{
		Task<PessoaFisicaDto> ObterAsync();
	}
}
