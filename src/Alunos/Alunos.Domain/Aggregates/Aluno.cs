using SharedKernel.Common;
using System;

namespace Alunos.Domain.Aggregates
{
	public class Aluno : Entity<Guid>
	{
		public int Id { get; private set; }
		public DateTime DataCriacao { get; private set; }
	}
}
