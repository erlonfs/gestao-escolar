using System;
using System.Collections.Generic;

namespace Demo.GestaoEscolar.Infra.Data
{
	public class EscolaDto
	{
		public int Id { get; set; }
		public Guid EntityId { get; set; }
		public DateTime DataCriacao { get; set; }

		public string Nome { get; set; }

		public IEnumerable<SalaDto> Salas { get; set; }
	}

	public class SalaDto
	{
		public Guid EntityId { get; set; }
		public string FaseAno { get; set; }
		public string Turno { get; set; }
		public int QtdAlunos { get; set; }
	}
}
