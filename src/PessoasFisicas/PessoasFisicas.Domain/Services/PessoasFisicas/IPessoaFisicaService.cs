using Demo.GerenciamentoEscolar.Domain.Aggregates.PessoasFisicas;
using System;
using System.Threading.Tasks;

namespace Demo.GerenciamentoEscolar.Domain.Services.PessoasFisicas
{
	public interface IPessoaFisicaService
	{
		Task<PessoaFisica> CriarAsync(Guid id, string nome, string cpf, string nomeSocial, string sexo, DateTime dataNascimento);
	}
}
