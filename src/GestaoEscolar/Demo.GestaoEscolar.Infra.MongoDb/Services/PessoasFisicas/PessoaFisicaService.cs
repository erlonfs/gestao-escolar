using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Exceptions.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Repositories.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Services.PessoasFisicas;
using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.MongoDb.Services.PessoasFisicas
{
	public class PessoaFisicaService : IPessoaFisicaService
	{
		private readonly IPessoaFisicaRepository _pessoasFisicaRepository;

		public PessoaFisicaService(IPessoaFisicaRepository pessoaFisicaRepository)
		{
			_pessoasFisicaRepository = pessoaFisicaRepository;
		}

		public async Task CriarAsync(Guid id, string nome, string cpf, string nomeSocial, string sexo, DateTime dataNascimento)
		{
			var pessoaFisica = new PessoaFisica(id, nome, cpf, nomeSocial, sexo, dataNascimento);
			await _pessoasFisicaRepository.AddAsync(pessoaFisica);
		}

		public async Task AlterarCpfAsync(Guid id,string cpf)
		{
			var pessoaFisica = await _pessoasFisicaRepository.GetByEntityIdAsync(id);

			var pessoaComCpfASerAlterado = await _pessoasFisicaRepository.ObterPorCpfAsync(cpf);
			if (pessoaComCpfASerAlterado != null) throw new PessoaFisicaCpfJaExistenteException();

			pessoaFisica.AlterarCpf(cpf);

			await _pessoasFisicaRepository.UpdateAsync(pessoaFisica);

		}
	}
}
