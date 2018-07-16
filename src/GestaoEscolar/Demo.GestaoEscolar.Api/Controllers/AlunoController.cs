using Demo.GestaoEscolar.Domain.Services.Alunos;
using Demo.GestaoEscolar.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Api.Controllers
{
	[Produces("application/json")]
	[Route("api/alunos")]
	public class AlunoController : BaseApiController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAlunoService _alunoService;
		private readonly IAlunoFinder _alunoFinder;

		public AlunoController(IUnitOfWork unitOfWork,
							   IAlunoService alunoService,
							   IAlunoFinder alunoFinder)
		{
			_unitOfWork = unitOfWork;
			_alunoService = alunoService;
			_alunoFinder = alunoFinder;
		}

		[HttpPost]
		[Route("/matricular")]
		public async Task<Guid> MatricularAsync(Guid pessoaFisicaId, Guid responsavelId, Guid escolaId, Guid salaId)
		{
			var id = Guid.NewGuid();

			await _alunoService.MatricularAsync(id, pessoaFisicaId, responsavelId, escolaId, salaId);
			await _unitOfWork.CommitAsync();

			return id;

		}

		[HttpGet]
		[Route("")]
		public async Task<IEnumerable<AlunoDto>> ObterAsync()
		{
			var result = await _alunoFinder.ObterAsync();
			if (result == null || !result.Any()) NotFound();

			return result;

		}

		[HttpPut]
		[Route("{id:guid}/rematricular")]
		public async Task<Guid> ReMatricularAsync(Guid id, Guid responsavelId, Guid escolaId, Guid salaId)
		{
			await _alunoService.RematricularAsync(id, responsavelId, escolaId, salaId);
			await _unitOfWork.CommitAsync();

			return id;

		}

		[HttpPut]
		[Route("{id:guid}/transferir")]
		public async Task<Guid> TransferirAsync(Guid id)
		{
			await _alunoService.TransferirAsync(id);
			await _unitOfWork.CommitAsync();

			return id;

		}
	}
}
