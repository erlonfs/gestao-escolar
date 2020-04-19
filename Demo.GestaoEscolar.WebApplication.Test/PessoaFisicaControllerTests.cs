using Demo.GestaoEscolar.Domain.Finders.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Demo.GestaoEscolar.WebApplication.Test
{
	[Collection("Sequential")]
	public class PessoaFisicaControllerTests : TestBase
	{
		private HttpClient _httpCliente;

		public PessoaFisicaControllerTests(ProgramTest<FakeStartup> factory) : base(factory)
		{
			_httpCliente = Factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false,
			});
		}

		[Theory]
		[InlineData("api/pessoas-fisicas/")]
		public async Task Quando_obter_pessoas_fisicas_devera_constar_dto_de_retorno(string url)
		{
			var response = await _httpCliente.GetAsync(url);

			var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
			var dtos = JsonConvert.DeserializeObject<IEnumerable<PessoaFisicaDto>>(result);

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			dtos.First().Cpf.Should().Be("81337257630");
		}

	}
}


