using Demo.GestaoEscolar.Domain.Finders.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Demo.GestaoEscolar.WebApplication.Test
{
	[Collection("Sequential")]
	public class Quando_criar_pessoa_fisica : TestBase
	{
		private HttpClient _httpClient;

		public Quando_criar_pessoa_fisica(ProgramTest<FakeStartup> factory) : base(factory)
		{
			_httpClient = Factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false
			});
		}

		[Fact]
		public async Task Devera_retornar_id()
		{
			var dto = new PessoaFisicaDto
			{
				Cpf = "03443703135",
				DataNascimento = new DateTime(1990, 04, 02),
				Nome = "Erlon",
				Sexo = "M"
			};

			var response = await _httpClient.PostAsJsonAsync("api/pessoas-fisicas/", dto);
			var result = await response.Content.ReadAsStringAsync();

			var pessoaFisicaId = JsonConvert.DeserializeObject<Guid>(result);

			pessoaFisicaId.Should().NotBeEmpty();
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}
	}
}


