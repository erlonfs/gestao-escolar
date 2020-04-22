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
	public class AlunoControllerTest : TestBase
	{
		private HttpClient _httpClient;
		private static Guid _alunoId;
		private static Guid _pessoaFisicaId;
		private static PessoaFisicaDto _pessoaFisicaDto;

		private static Guid _responsavelId;
		private static PessoaFisicaDto _responsavelDto;

		private static Guid _responsavelRematriculaId;
		private static PessoaFisicaDto _responsavelRematriculaDto;

		private static Guid _escolaId;
		private static string _nomeEscola;

		private static Guid _salaId;

		public AlunoControllerTest(ProgramTest<FakeStartup> factory) : base(factory)
		{
			_httpClient = Factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false
			});
		}

		private async Task Criar_pessoa_fisica_responsavel_rematricula()
		{
			_responsavelRematriculaDto = new PessoaFisicaDto
			{
				Cpf = "90611393417",
				DataNascimento = new DateTime(1964, 02, 10),
				Nome = "Vanessa Aline Corte Real",
				Sexo = "F"
			};

			var response = await _httpClient.PostAsJsonAsync("api/pessoas-fisicas/", _responsavelRematriculaDto);
			var result = await response.Content.ReadAsStringAsync();

			_responsavelRematriculaId = JsonConvert.DeserializeObject<Guid>(result);
		}

		private async Task Criar_pessoa_fisica_responsavel()
		{
			_responsavelDto = new PessoaFisicaDto
			{
				Cpf = "21520659725",
				DataNascimento = new DateTime(1959, 01, 11),
				Nome = "Thiago Julio Martins",
				Sexo = "M"
			};

			var response = await _httpClient.PostAsJsonAsync("api/pessoas-fisicas/", _responsavelDto);
			var result = await response.Content.ReadAsStringAsync();

			_responsavelId = JsonConvert.DeserializeObject<Guid>(result);
		}

		private async Task Criar_pessoa_fisica()
		{
			_pessoaFisicaDto = new PessoaFisicaDto
			{
				Cpf = "30839452055",
				DataNascimento = new DateTime(2005, 04, 02),
				Nome = "Lucca Ricardo Porto",
				Sexo = "M"
			};

			var response = await _httpClient.PostAsJsonAsync("api/pessoas-fisicas/", _pessoaFisicaDto);
			var result = await response.Content.ReadAsStringAsync();

			_pessoaFisicaId = JsonConvert.DeserializeObject<Guid>(result);
		}

		private async Task Criar_escola()
		{
			_nomeEscola = "Escola de testes";

			var response = await _httpClient.PostAsJsonAsync<string>($"api/escolas/?nome={_nomeEscola}", null);
			var result = await response.Content.ReadAsStringAsync();

			_escolaId = JsonConvert.DeserializeObject<Guid>(result);
		}

		private async Task Adicionar_sala_na_escola()
		{
			var faseAno = "1º ANO";
			var turno = (int)Turno.Matutino;

			var response = await _httpClient.PostAsync($"api/escolas/{_escolaId}/sala/?faseAno={faseAno}&turno={turno}", null);
			var result = await response.Content.ReadAsStringAsync();

			_salaId = JsonConvert.DeserializeObject<Guid>(result);
		}

		[Fact, Order(0)]
		public async Task Deve_criar_prerequisitos()
		{
			await Criar_escola();
			await Adicionar_sala_na_escola();
			await Criar_pessoa_fisica_responsavel();
			await Criar_pessoa_fisica_responsavel_rematricula();
			await Criar_pessoa_fisica();
		}

		[Fact, Order(1)]
		public async Task Quando_matricular_um_aluno___devera_retornar_id()
		{
			var response = await _httpClient.PostAsJsonAsync<string>($"api/alunos/matricular/?pessoaFisicaId={_pessoaFisicaId}&responsavelId={_responsavelId}&escolaId={_escolaId}&salaId={_salaId}", null);
			var result = await response.Content.ReadAsStringAsync();

			_alunoId = JsonConvert.DeserializeObject<Guid>(result);

			_alunoId.Should().NotBeEmpty();
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		[Fact, Order(2)]
		public async Task Quando_transferir_um_aluno___devera_retornar_id()
		{
			var response = await _httpClient.PutAsJsonAsync<string>($"api/alunos/{_alunoId}/transferir/", null);
			var result = await response.Content.ReadAsStringAsync();

			_alunoId = JsonConvert.DeserializeObject<Guid>(result);

			_alunoId.Should().NotBeEmpty();
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		[Fact, Order(3)]
		public async Task Quando_rematricular_um_aluno___devera_retornar_id()
		{
			var response = await _httpClient.PutAsJsonAsync<string>($"api/alunos/{_alunoId}/rematricular?responsavelId={_responsavelRematriculaId}&escolaId={_escolaId}&salaId={_salaId}", null);
			var result = await response.Content.ReadAsStringAsync();

			_alunoId = JsonConvert.DeserializeObject<Guid>(result);

			_alunoId.Should().NotBeEmpty();
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		[Fact, Order(4)]
		public async Task Quando_obter_alunos___devera_retornar_dados()
		{
			var response = await _httpClient.GetAsync($"api/alunos/");
			var result = await response.Content.ReadAsStringAsync();

			var dtoRetorno = JsonConvert.DeserializeObject<IEnumerable<AlunoDto>>(result).First();

			response.StatusCode.Should().Be(HttpStatusCode.OK);

			dtoRetorno.EntityId.Should().Be(_alunoId);
			dtoRetorno.DataCriacao.Date.Should().Be(DateTime.Today);
			dtoRetorno.Matricula.Should().NotBe(0);
			dtoRetorno.SituacaoId.Should().NotBe(0);
			dtoRetorno.Situacao.Should().NotBeNullOrEmpty();
			dtoRetorno.PessoaFisicaId.Should().NotBeEmpty();
			dtoRetorno.PessoaFisica.Should().NotBeNull();
			dtoRetorno.ResponsavelId.Should().NotBeEmpty();
			dtoRetorno.Responsavel.Should().NotBeNull();
			dtoRetorno.EscolaId.Should().NotBeEmpty();
			dtoRetorno.Escola.Should().NotBeNullOrEmpty();
			dtoRetorno.SalaId.Should().NotBeEmpty();
			dtoRetorno.Sala.Should().NotBeNullOrEmpty();


		}
	}
}


