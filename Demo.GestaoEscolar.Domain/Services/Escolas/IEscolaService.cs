using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Domain.Services.Escolas
{
	public interface IEscolaService
	{
		Task CriarAsync(Guid escolaId, string nome);
		Task AdicionarSalaAsync(Guid escolaId, Guid salaId, string faseAno, Turno turno);
		Task RemoverSalaAsync(Guid escolaId, Guid salaId);
	}
}
