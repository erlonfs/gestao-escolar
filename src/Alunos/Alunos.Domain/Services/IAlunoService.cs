using Alunos.Domain.Aggregates;
using System;
using System.Threading.Tasks;

namespace Alunos.Domain.Services
{
	public interface IAlunoService
	{
		Task<Aluno> CriarAsync(Guid id, int pessoaFisicaId, int matricula);
	}
}
