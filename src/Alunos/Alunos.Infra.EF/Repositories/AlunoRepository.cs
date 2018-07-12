using Alunos.Domain.Aggregates;
using Alunos.Domain.Repositories;

namespace Alunos.Infra.EF.Repositories
{
	public class AlunoRepository : Repository<Aluno>, IAlunoRepository
	{
		private AlunosDbContext _context;

		public AlunoRepository(AlunosDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
