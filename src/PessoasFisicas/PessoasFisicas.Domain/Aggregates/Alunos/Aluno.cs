using Demo.GerenciamentoEscolar.Domain.Aggregates.PessoasFisicas;
using SharedKernel.Common;
using System;

namespace Demo.GerenciamentoEscolar.Domain.Aggregates.Alunos
{
	public class Aluno : Entity<Guid>
    {
		public int Id { get; private set; }
		public DateTime DataCriacao { get; private set; }

		public int PessoaFisicaId { get; private set; }
		public virtual PessoaFisica PessoaFisica { get; private set; }

		public int Matricula { get; private set; }

		protected Aluno()
		{

		}

		public Aluno(Guid id, PessoaFisica pessoaFisica, int matricula)
		{
			EntityId = id;
			DataCriacao = DateTime.Now;
			PessoaFisica = pessoaFisica;
			Matricula = matricula;
		}
	}
}
