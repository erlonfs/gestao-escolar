using System;

namespace Demo.GestaoEscolar.Domain.Finders.Dtos
{
	public class PessoaFisicaDto
	{
		public Guid EntityId { get; set; }
		public DateTime DataCriacao { get; set; }

		public string Nome { get; set; }
		public string Cpf { get; set; }
		public string NomeSocial { get; set; }
		public string Sexo { get; set; }
		public DateTime DataNascimento { get; set; }
	}
}
