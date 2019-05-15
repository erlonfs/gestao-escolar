using CrossCutting;
using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.Alunos;
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

        public Task HandleAsync(AlunoMatriculado e)
        {
            return Task.CompletedTask;
        }
    }
}
