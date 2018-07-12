using Alunos.Domain.Aggregates;
using Alunos.Domain.Repositories;
using Alunos.Domain.Services;
using System;
using System.Threading.Tasks;

namespace Alunos.Infra.EF.Services
{
	public class AlunoService : IAlunoService
	{
		private readonly IAlunoRepository _alunoRepository;

		public AlunoService(IAlunoRepository alunoRepository)
		{
			_alunoRepository = alunoRepository;
		}

		public async Task<Aluno> CriarAsync(Guid id, int pessoaFisicaId, int matricula)
		{
			var aluno = new Aluno(id, DateTime.Now, pessoaFisicaId, matricula);

			await _alunoRepository.AddAsync(aluno);

			return aluno;
		}
	}
}
