using System;

namespace Demo.GestaoEscolar.Domain.Finders.Dtos
{
	public class AlunoDto
	{
		public Guid EntityId { get; set; }
		public DateTime DataCriacao { get; set; }
		public int Matricula { get; set; }

		public int SituacaoId { get; set; }
		public string Situacao { get; set; }

		public Guid PessoaFisicaId { get; set; }
		public PessoaFisicaDto PessoaFisica { get; set; }

		public Guid ResponsavelId { get; set; }
		public PessoaFisicaDto Responsavel { get; set; }

		public Guid EscolaId { get; set; }
		public string Escola { get; set; }

		public Guid SalaId { get; set; }
		public string Sala { get; set; }

	}
}
