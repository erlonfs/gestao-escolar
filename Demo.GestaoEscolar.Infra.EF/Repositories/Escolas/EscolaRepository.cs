using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Demo.GestaoEscolar.Domain.Repositories.Escolas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.EF.Repositories.Escolas
{
	public class EscolaRepository : Repository<Escola>,  IEscolaRepository
	{
		private AppDbContext _context;

		public EscolaRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<Escola> ObterPorAlunoIdAsync(Guid alunoId)
		{
			return await _context.Escola.SingleOrDefaultAsync(x => x.Salas.Any(y => y.Alunos.Any(z => z.Aluno.EntityId == alunoId)));
		}

		public async Task RemoverAlunoPorAsync(Guid escolaId, Guid alunoId)
		{
			var escola = await _context.Escola.SingleOrDefaultAsync(x => x.EntityId == escolaId);

			var sala = escola.Salas.SingleOrDefault(x => x.Alunos.Any(y => y.Aluno.EntityId == alunoId));

			_context.SalaAluno.Remove(sala.Alunos.SingleOrDefault(x => x.Aluno.EntityId == alunoId));

		}
	}
}
