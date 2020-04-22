using Demo.GestaoEscolar.Domain.Aggregates.Escolas;
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
	public class EscolaControllerTest : TestBase
	{
		private HttpClient _httpClient;
		private static Guid _escolaId;
		private static string _nome;
		private static Guid _salaId;

		public EscolaControllerTest(ProgramTest<FakeStartup> factory) : base(factory)
		{
			_httpClient = Factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false
			});
		}

		[Fact, Order(1)]
		public async Task Quando_criar_escola___devera_retornar_id()
		{
			_nome = "Escola de testes";

			var response = await _httpClient.PostAsJsonAsync<string>($"api/escolas/?nome={_nome}", null);
			var result = await response.Content.ReadAsStringAsync();

			_escolaId = JsonConvert.DeserializeObject<Guid>(result);

			_escolaId.Should().NotBeEmpty();
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		[Fact, Order(2)]
		public async Task Quando_adicionar_sala___devera_retornar_id_da_sala()
		{
			var faseAno = "1º ANO";
			var turno = (int)Turno.Matutino;

			var response = await _httpClient.PostAsync($"api/escolas/{_escolaId}/sala/?faseAno={faseAno}&turno={turno}", null);
			var result = await response.Content.ReadAsStringAsync();

			_salaId = JsonConvert.DeserializeObject<Guid>(result);

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			_salaId.Should().NotBeEmpty();
		}

		[Fact, Order(3)]
		public async Task Quando_obter_escolas___devera_retornar_dados()
		{
			var response = await _httpClient.GetAsync($"api/escolas/");
			var result = await response.Content.ReadAsStringAsync();

			var dtoRetorno = JsonConvert.DeserializeObject<IEnumerable<EscolaDto>>(result).First();

			response.StatusCode.Should().Be(HttpStatusCode.OK);

			dtoRetorno.EntityId.Should().Be(_escolaId);
			dtoRetorno.DataCriacao.Date.Should().Be(DateTime.Today);
			dtoRetorno.Nome.Should().Be(_nome);
			dtoRetorno.Salas.First().EntityId.Should().Be(_salaId);
		}
	}
}


