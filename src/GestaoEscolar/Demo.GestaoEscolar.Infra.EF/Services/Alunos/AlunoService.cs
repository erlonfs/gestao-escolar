using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Exceptions.Alunos;
using Demo.GestaoEscolar.Domain.Exceptions.Escolas;
using Demo.GestaoEscolar.Domain.Exceptions.PessoasFisicas;
using Demo.GestaoEscolar.Domain.Repositories.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.Escolas;
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
		private readonly IEscolaRepository _escolaRepository;
		private readonly IMatriculaService _matriculaService;

		public AlunoService(IAlunoRepository alunoRepository,
							IPessoaFisicaRepository pessoaFisicaRepository,
							IEscolaRepository escolaRepository,
							IMatriculaService matriculaService)
		{
			_alunoRepository = alunoRepository;
			_pessoaFisicaRepository = pessoaFisicaRepository;
			_escolaRepository = escolaRepository;
			_matriculaService = matriculaService;
		}

		public async Task MatricularAsync(Guid id, Guid pessoaFisicaId, Guid responsavelId, Guid escolaId, Guid salaId)
		{
			var pessoaFisica = await _pessoaFisicaRepository.GetByEntityIdAsync(pessoaFisicaId);
			if (pessoaFisica == null) throw new PessoaFisicaNaoEncontradaException();

			var responsavel = await _pessoaFisicaRepository.GetByEntityIdAsync(responsavelId);
			if (responsavel == null) throw new ResponsavelNaoEncontradaException();

			var escola = await _escolaRepository.GetByEntityIdAsync(escolaId);
			if (escola == null) throw new EscolaNaoEncontradaException();

			var matricula = await _matriculaService.GerarMatriculaAsync();
			var aluno = new Aluno(id, pessoaFisica, responsavel, matricula);

			escola.AdicionarAluno(salaId, aluno);

			await _alunoRepository.AddAsync(aluno);

		}
	}
}
