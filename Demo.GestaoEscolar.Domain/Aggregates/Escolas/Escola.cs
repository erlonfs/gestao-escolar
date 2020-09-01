using CrossCutting;
using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Demo.GestaoEscolar.Domain.Test")]
namespace Demo.GestaoEscolar.Domain.Aggregates.Escolas
{
    public class Escola : Aggregate<Guid>
	{
		public int Id { get; private set; }
		public DateTime DataCriacao { get; private set; }

		public string Nome { get; private set; }

		public virtual HashSet<Sala> Salas { get; private set; } = new HashSet<Sala>();

		protected Escola()
		{
			
		}

		internal Escola(Guid id, string nome)
		{
			EntityId = id;
			DataCriacao = DateTime.Now;
			Nome = nome;
		}

		internal void AdicionarSala(Guid salaId, string faseAno, Turno turno)
		{
			Salas.Add(new Sala(salaId, faseAno, turno));
		}

		internal void AdicionarAluno(Guid salaId, Aluno aluno)
		{
			var sala = Salas.SingleOrDefault(x => x.EntityId == salaId);
			sala.AdicionarAluno(aluno);
		}

		internal void RemoverAluno(Guid salaId, Aluno aluno)
		{
			var sala = Salas.SingleOrDefault(x => x.EntityId == salaId);
			sala.RemoverAluno(aluno);
		}

	}
}