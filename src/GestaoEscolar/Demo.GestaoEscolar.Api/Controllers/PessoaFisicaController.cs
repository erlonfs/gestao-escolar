using Demo.GestaoEscolar.Api.Dtos;
using Demo.GestaoEscolar.Domain.Services.PessoasFisicas;
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
	[Route("api/pessoas-fisicas")]
	public class PessoaFisicaController : BaseApiController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPessoaFisicaService _pessoaFisicaService;
		private readonly IPessoaFisicaFinder _pessoaFisicaFinder;

		public PessoaFisicaController(IUnitOfWork unitOfWork,
									 IPessoaFisicaService pessoaFisicaService,
									 IPessoaFisicaFinder pessoaFisicaFinder)
		{
			_unitOfWork = unitOfWork;
			_pessoaFisicaService = pessoaFisicaService;
			_pessoaFisicaFinder = pessoaFisicaFinder;
		}

		[HttpPost]
		[Route("")]
		public async Task<Guid> CriarAsync(CriarPessoaFisicaDto dto)
		{
			var id = Guid.NewGuid();

			await _pessoaFisicaService.CriarAsync(id, dto.Nome, dto.Cpf, dto.NomeSocial, dto.Sexo, dto.DataNascimento);
			await _unitOfWork.CommitAsync();

			return id;

		}

		[HttpGet]
		[Route("")]
		public async Task<IEnumerable<PessoaFisicaDto>> ObterAsync()
		{
			var result = await _pessoaFisicaFinder.ObterAsync();
			if (result == null || !result.Any()) NotFound();

			return result;

		}
	}
}
