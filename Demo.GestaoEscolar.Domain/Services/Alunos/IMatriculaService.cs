using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Domain.Services.Alunos
{
	public interface IMatriculaService
    {
		Task<int> GerarMatriculaAsync();
	}
}
