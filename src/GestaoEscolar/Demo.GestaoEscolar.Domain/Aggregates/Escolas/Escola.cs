using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace Demo.GestaoEscolar.Domain.Aggregates.Escolas
{
	public class Escola : Aggregate<Guid>
	{
		public int Id { get; private set; }
		public DateTime DataCriacao { get; private set; }

		public virtual HashSet<Sala> Salas { get; private set; } = new HashSet<Sala>();

	}
}