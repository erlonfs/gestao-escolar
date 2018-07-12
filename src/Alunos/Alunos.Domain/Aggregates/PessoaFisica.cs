using System;

namespace Alunos.Domain.Aggregates
{
	public class PessoaFisica
	{
		public int Id { get; protected set; }
		public string Nome { get; protected set; }
		public string Cpf { get; protected set; }
		public string Sexo { get; protected set; }
		public DateTime DataNascimento { get; protected set; }
	}
}
