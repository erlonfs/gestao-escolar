using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Domain.Services.Alunos
{
	public interface IAlunoService
	{
		Task MatricularAsync(Guid id, Guid pessoaFisicaId, Guid responsavelId, Guid escolaId, Guid salaId);
	}
}
