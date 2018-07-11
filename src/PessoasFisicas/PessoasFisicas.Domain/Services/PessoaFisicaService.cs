using PessoasFisicas.Domain.Repositories;
using PessoasFisicas.Domain.Aggregates;
using System;
using System.Threading.Tasks;

namespace PessoasFisicas.Domain.Services
{
	public class PessoaFisicaService : IPessoaFisicaService
	{
		private readonly IPessoaFisicaRepository _pessoasFisicaRepository;

		public PessoaFisicaService(IPessoaFisicaRepository pessoaFisicaRepository)
		{
			_pessoasFisicaRepository = pessoaFisicaRepository;
		}

		public async Task<PessoaFisica> CriarAsync(Guid id, string nome, string cpf, string nomeSocial, string sexo, DateTime dataNascimento)
		{
			var pessoaFisica = new PessoaFisica(id, DateTime.Now, nome, cpf, nomeSocial, sexo, dataNascimento);

			await _pessoasFisicaRepository.AddAsync(pessoaFisica);

			return pessoaFisica;
		}
	}
}
