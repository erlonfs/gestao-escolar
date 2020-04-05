using CrossCutting;
using Demo.GestaoEscolar.Agregates.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.Alunos;
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

		public Task HandleAsync(AlunoTransferido e)
		{
			return Task.CompletedTask;
		}
	}
}
