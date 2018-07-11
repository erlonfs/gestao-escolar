using PessoasFisicas.Domain.Aggregates;
using System;
using System.Threading.Tasks;

namespace PessoasFisicas.Domain.Services
{
	public interface IPessoaFisicaService
	{
		Task<PessoaFisica> CriarAsync(Guid id, string nome, string cpf, string nomeSocial, string sexo, DateTime dataNascimento);
	}
}
