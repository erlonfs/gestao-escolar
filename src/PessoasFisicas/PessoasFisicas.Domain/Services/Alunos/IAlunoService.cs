using Demo.GerenciamentoEscolar.Domain.Aggregates.Alunos;
using System;
using System.Threading.Tasks;

namespace Demo.GerenciamentoEscolar.Domain.Services.Alunos
{
	public interface IAlunoService
	{
		Task<Aluno> CriarAsync(Guid id, Guid pessoasFisicaId, int matricula);
	}
}
