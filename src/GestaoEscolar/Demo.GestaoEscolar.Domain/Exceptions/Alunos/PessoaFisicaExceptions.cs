using System;

namespace Demo.GestaoEscolar.Domain.Exceptions.Alunos
{
	public class ResponsavelNaoEncontradaException : ApplicationException
	{
		public ResponsavelNaoEncontradaException() : base("Responsável não encontrado.") { }
	}
}
