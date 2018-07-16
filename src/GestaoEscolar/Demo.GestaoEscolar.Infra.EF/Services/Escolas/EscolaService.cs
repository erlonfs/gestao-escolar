using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Demo.GestaoEscolar.Domain.Repositories.Escolas;
using Demo.GestaoEscolar.Domain.Services.Escolas;
using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.EF.Services.Escolas
{
	public class EscolaService : IEscolaService
	{
		private readonly IEscolaRepository _escolaRepository;

		public EscolaService(IEscolaRepository escolaRepository)
		{
			_escolaRepository = escolaRepository;
		}

		public async Task CriarAsync(Guid escolaId, string nome)
		{
			var escola = new Escola(escolaId, nome);

			await _escolaRepository.AddAsync(escola);

		}

		public async Task AdicionarSalaAsync(Guid escolaId, Guid salaId, string faseAno, Turno turno)
		{
			var escola = await _escolaRepository.GetByEntityIdAsync(escolaId);
			escola.AdicionarSala(salaId, faseAno, turno);
		}

		public Task RemoverSalaAsync(Guid escolaId, Guid salaId)
		{
			throw new NotImplementedException();
		}
	}
}
