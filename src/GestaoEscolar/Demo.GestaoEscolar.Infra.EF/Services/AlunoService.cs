using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Services.Alunos;
using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.EF.Services
{
	public class AlunoService : IAlunoService
	{
		private readonly IAlunoRepository _alunoRepository;
		private readonly IPessoaFisicaRepository _pessoaFisicaRepository;

		public AlunoService(IAlunoRepository alunoRepository, IPessoaFisicaRepository pessoaFisicaRepository)
		{
			_alunoRepository = alunoRepository;
			_pessoaFisicaRepository = pessoaFisicaRepository;
		}

		public async Task<Aluno> CriarAsync(Guid id, Guid pessoaFisicaId, int matricula)
		{
			var pessoaFisica = await _pessoaFisicaRepository.GetByEntityIdAsync(pessoaFisicaId);

			var aluno = new Aluno(id, pessoaFisica, matricula);

			await _alunoRepository.AddAsync(aluno);

			return aluno;
		}
	}
}
