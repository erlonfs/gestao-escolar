using Demo.GestaoEscolar.Domain.Finders.Dtos;
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
using Xunit.Extensions.Ordering;

namespace Demo.GestaoEscolar.WebApplication.Test
{
	public class PessoaFisicaControllerTest : TestBase
	{
		private HttpClient _httpClient;
		private static Guid _pessoaFisicaId;
		private static PessoaFisicaDto _dto;

		public PessoaFisicaControllerTest(ProgramTest<FakeStartup> factory) : base(factory)
		{
			_httpClient = Factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false
			});
		}

		[Fact, Order(1)]
		public async Task Quando_criar_pessoa_fisica___devera_retornar_id()
		{
			_dto = new PessoaFisicaDto
			{
				Cpf = "30839452055",
				DataNascimento = new DateTime(2005, 04, 02),
				Nome = "Lucca Ricardo Porto",
				Sexo = "M"
			};

			var response = await _httpClient.PostAsJsonAsync("api/pessoas-fisicas/", _dto);
			var result = await response.Content.ReadAsStringAsync();

			_pessoaFisicaId = JsonConvert.DeserializeObject<Guid>(result);

			_pessoaFisicaId.Should().NotBeEmpty();
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		[Fact, Order(2)]
		public async Task Quando_alterar_cpf_pessoa_fisica___devera_alterar_cpf_e_retornar_id()
		{
			var novoCpf = "55558185139";

			var responseAlterarCpf = await _httpClient.PutAsync($"api/pessoas-fisicas/{_pessoaFisicaId}/alterar-cpf/?cpf={novoCpf}", null);
			var resultAlterar = await responseAlterarCpf.Content.ReadAsStringAsync();

			var pessoaFisicaCpfAlteradoId = JsonConvert.DeserializeObject<Guid>(resultAlterar);

			responseAlterarCpf.StatusCode.Should().Be(HttpStatusCode.OK);
			pessoaFisicaCpfAlteradoId.Should().Be(_pessoaFisicaId);

			_dto.Cpf = novoCpf;
		}

		[Fact, Order(3)]
		public async Task Quando_obter_pessoas_fisicas___devera_retornar_dados()
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


