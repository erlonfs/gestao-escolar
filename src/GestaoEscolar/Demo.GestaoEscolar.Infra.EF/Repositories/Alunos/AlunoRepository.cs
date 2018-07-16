using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.Alunos;

namespace Demo.GestaoEscolar.Infra.EF.Repositories.Alunos
{
	public class AlunoRepository : Repository<Aluno>, IAlunoRepository
	{
		private AppDbContext _context;

		public AlunoRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
