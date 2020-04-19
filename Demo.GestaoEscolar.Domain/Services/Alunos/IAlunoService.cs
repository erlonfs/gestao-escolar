using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Domain.Services.Alunos
{
	public interface IAlunoService
	{
		Task MatricularAsync(Guid alunoId, Guid pessoaFisicaId, Guid responsavelId, Guid escolaId, Guid salaId);
		Task RematricularAsync(Guid alunoId, Guid responsavelId, Guid escolaId, Guid salaId);
		Task TransferirAsync(Guid alunoId);
	}
}
