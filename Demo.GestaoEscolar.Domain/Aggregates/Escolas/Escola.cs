using CrossCutting;
using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.GestaoEscolar.Domain.Aggregates.Escolas
{
    public class Escola : Aggregate<Guid>
	{
		public int Id { get; private set; }
		public DateTime DataCriacao { get; private set; }

		public string Nome { get; private set; }

		public virtual HashSet<Sala> Salas { get; private set; } = new HashSet<Sala>();

		public IDomainEventsBag DomainEventsBag { get; set; }

		protected Escola()
		{
			
		}

		public Escola(Guid id, string nome)
		{
			EntityId = id;
			DataCriacao = DateTime.Now;
			Nome = nome;
		}

		public void AdicionarSala(Guid salaId, string faseAno, Turno turno)
		{
			Salas.Add(new Sala(salaId, faseAno, turno));
		}

		public void AdicionarAluno(Guid salaId, Aluno aluno)
		{
			var sala = Salas.SingleOrDefault(x => x.EntityId == salaId);
			sala.AdicionarAluno(aluno);
		}

		public void RemoverAluno(Guid salaId, Aluno aluno)
		{
			var sala = Salas.SingleOrDefault(x => x.EntityId == salaId);
			sala.RemoverAluno(aluno);
		}

	}
}