using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.Alunos;
using SharedKernel.Common;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Handlers.Alunos
{
	public class AlunoMatriculadoHandler : IHandler<AlunoMatriculado>
	{
		private IAlunoRepository _alunoRepository;

		public AlunoMatriculadoHandler(IAlunoRepository alunoRepository)
		{
			_alunoRepository = alunoRepository;
		}

		public async Task HandleAsync(AlunoMatriculado e)
		{

		}
	}
}
