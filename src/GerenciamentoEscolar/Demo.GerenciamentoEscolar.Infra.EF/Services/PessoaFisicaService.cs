using Demo.GerenciamentoEscolar.Domain.Aggregates.PessoasFisicas;
using Demo.GerenciamentoEscolar.Domain.Repositories.PessoasFisicas;
using Demo.GerenciamentoEscolar.Domain.Services.PessoasFisicas;
using System;
using System.Threading.Tasks;

namespace Demo.GerenciamentoEscolar.Infra.EF.Services
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
			var pessoaFisica = new PessoaFisica(id, nome, cpf, nomeSocial, sexo, dataNascimento);

			await _pessoasFisicaRepository.AddAsync(pessoaFisica);

			return pessoaFisica;
		}
	}
}
