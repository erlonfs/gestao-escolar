using CrossCutting;
using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using System;

namespace Demo.GestaoEscolar.Agregates.PessoasFisicas
{
    public class PessoaFisicaAlterada : IDomainEvent
	{
		public Guid AggregateId { get; }
		public PessoaFisica PessoaFisica { get; }
		public DateTime DataExecucao { get; }

		public PessoaFisicaAlterada(Guid aggregateId, PessoaFisica pessoaFisica)
		{
			AggregateId = aggregateId;
			PessoaFisica = pessoaFisica;
			DataExecucao = DateTime.Now;
		}
	}
}
