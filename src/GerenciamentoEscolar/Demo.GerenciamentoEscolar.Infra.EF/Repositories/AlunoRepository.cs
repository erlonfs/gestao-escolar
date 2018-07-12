using Demo.GerenciamentoEscolar.Domain.Aggregates.Alunos;
using Demo.GerenciamentoEscolar.Domain.Repositories.Alunos;

namespace Demo.GerenciamentoEscolar.Infra.EF.Repositories
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
