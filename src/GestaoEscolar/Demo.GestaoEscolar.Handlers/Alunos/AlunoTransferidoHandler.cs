using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.Alunos;
using SharedKernel.Common;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Handlers.Alunos
{
	public class AlunoTransferidoHandler : IHandler<AlunoTransferido>
	{
		private IAlunoRepository _alunoRepository;

		public AlunoTransferidoHandler(IAlunoRepository alunoRepository)
		{
			_alunoRepository = alunoRepository;
		}

		public async Task HandleAsync(AlunoTransferido e)
		{

		}
	}
}
