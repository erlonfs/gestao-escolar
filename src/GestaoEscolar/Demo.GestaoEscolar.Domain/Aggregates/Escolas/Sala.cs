using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace Demo.GestaoEscolar.Domain.Aggregates.Escolas
{
	public class Sala : Entity<Guid>
	{
		public int Id { get; private set; }
		public DateTime DataCriacao { get; private set; }

		public int EscolaId { get; private set; }
		public virtual Escola Escola { get; private set; }

		public FaseAno FaseAno { get; private set; }
		public Periodo Periodo { get; private set; }
		public Turno Turno { get; private set; }

		public virtual HashSet<Aluno> Alunos { get; private set; } = new HashSet<Aluno>();
	}
}
