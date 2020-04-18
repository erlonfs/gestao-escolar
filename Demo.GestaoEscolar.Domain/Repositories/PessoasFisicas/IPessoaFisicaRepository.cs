using CrossCutting;
using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Domain.Repositories.PessoasFisicas
{
    public interface IPessoaFisicaRepository : IRepository<PessoaFisica>
	{
		Task<PessoaFisica> ObterPorCpfAsync(string cpf);
	}
}
