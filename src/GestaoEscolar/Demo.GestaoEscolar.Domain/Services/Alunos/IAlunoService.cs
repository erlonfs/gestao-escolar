using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Domain.Services.Alunos
{
	public interface IAlunoService
	{
		Task<Aluno> CriarAsync(Guid id, Guid pessoasFisicaId, int matricula);
	}
}
