using Demo.GestaoEscolar.Domain.Services.Alunos;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Common;
using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Api.Controllers
{
	[Produces("application/json")]
	[Route("api/alunos")]
	public class AlunoController : BaseApiController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAlunoService _alunoService;

		public AlunoController(IUnitOfWork unitOfWork,
							   IAlunoService alunoService)
		{
			_unitOfWork = unitOfWork;
			_alunoService = alunoService;
		}

		[HttpPost]
		[Route("{pessoaFisicaId:guid}/matricular")]
		public async Task<Guid> MatricularAsync(Guid pessoaFisicaId, Guid responsavelId, Guid escolaId, Guid salaId)
		{
			var id = Guid.NewGuid();

			await _alunoService.MatricularAsync(id, pessoaFisicaId, responsavelId, escolaId, salaId);
			await _unitOfWork.CommitAsync();

			return id;

		}
	}
}
