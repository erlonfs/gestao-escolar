using Demo.GestaoEscolar.Domain.Finders.Dtos;
using Demo.GestaoEscolar.WebApplication.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Demo.GestaoEscolar.WebApplication.Test
{
	[Collection("Sequential")]
	public class Quando_obter_pessoa_fisica : TestBase
	{
		private HttpClient _httpClient;
		private Guid _pessoaFisicaId;
		private CriarPessoaFisicaDto _dto;

		public Quando_obter_pessoa_fisica(ProgramTest<FakeStartup> factory) : base(factory)
		{
			_httpClient = Factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false
			});

			_dto = new CriarPessoaFisicaDto
			{
				Cpf = "03443703135",
				DataNascimento = new DateTime(1990, 04, 02),
				Nome = "Erlon",
				Sexo = "M",
				NomeSocial = "Nome Social"
			};

			_pessoaFisicaId = create_pessoa_fisica().GetAwaiter().GetResult();
		}

		public async Task<Guid> create_pessoa_fisica()
		{
			var response = await _httpClient.PostAsJsonAsync("api/pessoas-fisicas/", _dto);
			var result = await response.Content.ReadAsStringAsync();

			var pessoaFisicaId = JsonConvert.DeserializeObject<Guid>(result);

			return pessoaFisicaId;
		}

		[Fact]
		public async Task Devera_retornar_dados()
		{
			var response = await _httpClient.GetAsync($"api/pessoas-fisicas/");
			var result = await response.Content.ReadAsStringAsync();

			var dtoRetorno = JsonConvert.DeserializeObject<IEnumerable<PessoaFisicaDto>>(result).First();

			response.StatusCode.Should().Be(HttpStatusCode.OK);

			dtoRetorno.EntityId.Should().Be(_pessoaFisicaId);
			dtoRetorno.DataCriacao.Date.Should().Be(DateTime.Today);
			dtoRetorno.Nome.Should().Be(_dto.Nome);
			dtoRetorno.Cpf.Should().Be(_dto.Cpf);
			dtoRetorno.NomeSocial.Should().Be(_dto.NomeSocial);
			dtoRetorno.Sexo.Should().Be(_dto.Sexo);
			dtoRetorno.DataNascimento.Should().Be(_dto.DataNascimento);
		}
	}


}


