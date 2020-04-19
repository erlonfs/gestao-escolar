using Demo.GestaoEscolar.Domain.Finders;
using Demo.GestaoEscolar.Domain.Finders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.WebApplication.Test
{
	public class PessaoFisicaFinderFake : IPessoaFisicaFinder
	{
		public Task<IEnumerable<PessoaFisicaDto>> ObterAsync()
		{
			var pessoas = new List<PessoaFisicaDto>();

			pessoas.Add(new PessoaFisicaDto
			{
				Cpf = "81337257630"
			});

			return Task.FromResult(pessoas.AsEnumerable());


		}
	}
}
