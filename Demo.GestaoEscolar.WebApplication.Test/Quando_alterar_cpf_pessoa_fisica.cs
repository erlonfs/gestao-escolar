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
	public class Quando_alterar_cpf_pessoa_fisica : TestBase
	{
		private HttpClient _httpClient;
		private Guid _pessoaFisicaId;

		public Quando_alterar_cpf_pessoa_fisica(ProgramTest<FakeStartup> factory) : base(factory)
		{
			_httpClient = Factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false
			});

			_pessoaFisicaId = create_pessoa_fisica().GetAwaiter().GetResult();
		}

		public async Task<Guid> create_pessoa_fisica()
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

			return pessoaFisicaId;
		}

		[Fact]
		public async Task Devera_alterar_cpf_e_retornar_id()
		{
			var novoCpf = "55558185139";

			var responseAlterarCpf = await _httpClient.PutAsync($"api/pessoas-fisicas/{_pessoaFisicaId}/alterar-cpf/?cpf={novoCpf}", null);
			var resultAlterar = await responseAlterarCpf.Content.ReadAsStringAsync();

			var pessoaFisicaCpfAlteradoId = JsonConvert.DeserializeObject<Guid>(resultAlterar);

			responseAlterarCpf.StatusCode.Should().Be(HttpStatusCode.OK);
			pessoaFisicaCpfAlteradoId.Should().Be(_pessoaFisicaId);
		}
	}


}


