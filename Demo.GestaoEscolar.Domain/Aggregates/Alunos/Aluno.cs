using CrossCutting;
using Demo.GestaoEscolar.Agregates.Alunos;
using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Demo.GestaoEscolar.Domain.Test")]
namespace Demo.GestaoEscolar.Domain.Aggregates.Alunos
{
	public class Aluno : Aggregate<Guid>
	{
		public int Id { get; private set; }
		public DateTime DataCriacao { get; private set; }

		public int PessoaFisicaId { get; private set; }
		public virtual PessoaFisica PessoaFisica { get; private set; }

		public int ResponsavelId { get; private set; }
		public virtual PessoaFisica Responsavel { get; private set; }

		public int Matricula { get; private set; }
		public int SituacaoId { get; private set; }

		protected Aluno()
		{

		}

		internal Aluno(Guid id, PessoaFisica pessoaFisica, PessoaFisica responsavel, int matricula)
		{
			EntityId = id;
			DataCriacao = DateTime.Now;
			PessoaFisica = pessoaFisica;
			Responsavel = responsavel;
			Matricula = matricula;
			SituacaoId = (int)AlunoSituacao.Matriculado;

			RaiseEvent(new AlunoMatriculado(EntityId, this));

		}

		internal void Rematricular(PessoaFisica responsavel)
		{
			Responsavel = responsavel;
			SituacaoId = (int)AlunoSituacao.Matriculado;

			RaiseEvent(new AlunoRematriculado(EntityId, this));
		}

		internal void Transferir()
		{
			SituacaoId = (int)AlunoSituacao.Transferido;

			RaiseEvent(new AlunoTransferido(EntityId, this));
		}
	}
}
