using System;

namespace Demo.GestaoEscolar.Domain.Exceptions.PessoasFisicas
{
	public class PessoaFisicaNaoEncontradaException : ApplicationException
	{
		public PessoaFisicaNaoEncontradaException() : base("Pessoa física não encontrada.") { }
	}
}
