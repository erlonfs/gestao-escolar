using System;
using System.Linq;
using System.Threading.Tasks;
using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Demo.GestaoEscolar.Domain.Repositories.Alunos;
using Microsoft.EntityFrameworkCore;

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
