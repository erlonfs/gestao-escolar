using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Domain.Services.PessoasFisicas
{
	public interface IPessoaFisicaService
	{
		Task CriarAsync(Guid id, string nome, string cpf, string nomeSocial, string sexo, DateTime dataNascimento);
	}
}
