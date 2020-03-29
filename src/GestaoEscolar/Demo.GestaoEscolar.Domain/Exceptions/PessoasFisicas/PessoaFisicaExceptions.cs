using System;

namespace Demo.GestaoEscolar.Domain.Exceptions.PessoasFisicas
{
	public class PessoaFisicaNaoEncontradaException : ApplicationException
	{
		public PessoaFisicaNaoEncontradaException() : base("Pessoa física não encontrada.") { }
	}

	public class PessoaFisicaCpfJaExistenteException : ApplicationException
	{
		public PessoaFisicaCpfJaExistenteException() : base("Existe uma pessoa física com o cpf informado.") { }
	}
}
