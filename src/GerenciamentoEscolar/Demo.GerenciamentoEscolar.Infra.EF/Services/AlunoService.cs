using Demo.GerenciamentoEscolar.Domain.Aggregates.Alunos;
using Demo.GerenciamentoEscolar.Domain.Repositories.Alunos;
using Demo.GerenciamentoEscolar.Domain.Repositories.PessoasFisicas;
using Demo.GerenciamentoEscolar.Domain.Services.Alunos;
using System;
using System.Threading.Tasks;

namespace Demo.GerenciamentoEscolar.Infra.EF.Services
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
