using System;

namespace Demo.GerenciamentoEscolar.Api.Dtos
{
	public class CriarPessoaFisicaDto
	{
		public string Nome { get; set; }
		public string Cpf { get; set; }
		public string NomeSocial { get; set; }
		public string Sexo { get; set; }
		public DateTime DataNascimento { get; set; }
	}
}
