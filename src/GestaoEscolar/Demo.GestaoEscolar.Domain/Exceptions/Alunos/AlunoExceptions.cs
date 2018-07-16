using System;

namespace Demo.GestaoEscolar.Domain.Exceptions.Alunos
{
	public class AlunoNaoEncontradoException : ApplicationException
	{
		public AlunoNaoEncontradoException() : base("Aluno não encontrado.") { }
	}

	public class AlunoJaMatriculadoException : ApplicationException
	{
		public AlunoJaMatriculadoException() : base("Aluno já está matriculado.") { }
	}

	public class AlunoNaoMatriculadoException : ApplicationException
	{
		public AlunoNaoMatriculadoException() : base("O Aluno não está matriculado.") { }
	}

	public class ResponsavelNaoEncontradoException : ApplicationException
	{
		public ResponsavelNaoEncontradoException() : base("Responsável não encontrado.") { }
	}
}
