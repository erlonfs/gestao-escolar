using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using SharedKernel.Common;
using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Domain.Repositories.Escolas
{
	public interface IEscolaRepository : IRepository<Escola>
	{
		Task<Escola> ObterPorAlunoIdAsync(Guid alunoId);
		Task RemoverAlunoPorAsync(Guid escolaId, Guid alunoId);
	}
}
