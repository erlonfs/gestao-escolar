using Demo.GestaoEscolar.Api.Dtos;
using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
using Demo.GestaoEscolar.Domain.Services.Escolas;
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
	[Route("api/escolas")]
	public class EscolaController : BaseApiController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IEscolaService _escolaService;
		private readonly IEscolaFinder _escolaFinder;

		public EscolaController(IUnitOfWork unitOfWork,
								IEscolaService escolaService,
								IEscolaFinder escolaFinder)
		{
			_unitOfWork = unitOfWork;
			_escolaService = escolaService;
			_escolaFinder = escolaFinder;
		}

		[HttpPost]
		[Route("")]
		public async Task<Guid> CriarAsync(string nome)
		{
			var id = Guid.NewGuid();

			await _escolaService.CriarAsync(id, nome);

			await _unitOfWork.CommitAsync();

			return id;

		}

		[HttpPost]
		[Route("{id:guid}/sala")]
		public async Task<Guid> AdicionarSalaAsync(Guid id, string faseAno, Turno turno)
		{
			var salaId = Guid.NewGuid();

			await _escolaService.AdicionarSalaAsync(id, salaId, faseAno, turno);

			await _unitOfWork.CommitAsync();

			return salaId;

		}

		[HttpGet]
		[Route("")]
		public async Task<IEnumerable<EscolaDto>> ObterAsync()
		{
			var result = await _escolaFinder.ObterAsync();
			if (result == null || !result.Any()) NotFound();

			return result;

		}
	}
}
