using Demo.GestaoEscolar.Api.Dtos;
using Demo.GestaoEscolar.Domain.Services.PessoasFisicas;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Common;
using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Api.Controllers
{
	[Produces("application/json")]
	[Route("api/pessoas-fisicas")]
	public class PessoaFisicaController : BaseApiController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPessoaFisicaService _pessoaFisicaService;

		public PessoaFisicaController(IUnitOfWork unitOfWork,
									 IPessoaFisicaService pessoaFisicaService)
		{
			_unitOfWork = unitOfWork;
			_pessoaFisicaService = pessoaFisicaService;
		}

		[HttpPost]
		[Route("")]
		public async Task<Guid> CriarAsync(CriarPessoaFisicaDto dto)
		{
			var id = Guid.NewGuid();

			var pessoaFisica = await _pessoaFisicaService.CriarAsync(id, dto.Nome, dto.Cpf, dto.NomeSocial, dto.Sexo, dto.DataNascimento);

			await _unitOfWork.CommitAsync();

			return id;

		}
	}
}
