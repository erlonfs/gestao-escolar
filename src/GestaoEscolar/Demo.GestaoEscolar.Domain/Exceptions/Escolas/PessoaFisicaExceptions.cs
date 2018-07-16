using System;

namespace Demo.GestaoEscolar.Domain.Exceptions.Escolas
{
	public class EscolaNaoEncontradaException : ApplicationException
	{
		public EscolaNaoEncontradaException() : base("Escola não encontrada.") { }
	}

	public class SalaNaoEncontradaException : ApplicationException
	{
		public SalaNaoEncontradaException() : base("Sala não encontrada.") { }
	}
}
