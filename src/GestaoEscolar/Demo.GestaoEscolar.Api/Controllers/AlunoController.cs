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
		public async Task<Guid> CriarAsync(Guid pessoaFisicaId)
		{
			var id = Guid.NewGuid();

			var aluno = await _alunoService.MatricularAsync(id, pessoaFisicaId);

			await _unitOfWork.CommitAsync();

			return id;

		}
	}
}
