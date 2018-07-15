using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Exceptions.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Repositories.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Services.Alunos;
using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.EF.Services.Alunos
{
	public class AlunoService : IAlunoService
	{
		private readonly IAlunoRepository _alunoRepository;
		private readonly IPessoaFisicaRepository _pessoaFisicaRepository;
		private readonly IMatriculaService _matriculaService;

		public AlunoService(IAlunoRepository alunoRepository, 
							IPessoaFisicaRepository pessoaFisicaRepository,
							IMatriculaService matriculaService)
		{
			_alunoRepository = alunoRepository;
			_pessoaFisicaRepository = pessoaFisicaRepository;
			_matriculaService = matriculaService;
		}

		public async Task MatricularAsync(Guid id, Guid pessoaFisicaId)
		{
			var pessoaFisica = await _pessoaFisicaRepository.GetByEntityIdAsync(pessoaFisicaId);
			if (pessoaFisica == null) throw new PessoaFisicaNaoEncontradaException();

			var matricula = await _matriculaService.GerarMatriculaAsync();

			var aluno = new Aluno(id, pessoaFisica, matricula);

			await _alunoRepository.AddAsync(aluno);
		}
	}
}
