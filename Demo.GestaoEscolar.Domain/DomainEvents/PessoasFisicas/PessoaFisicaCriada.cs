﻿using CrossCutting;
using Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas;
using System;

namespace Demo.GestaoEscolar.Agregates.PessoasFisicas
{
    public class PessoaFisicaCriada : IDomainEvent
	{
		public Guid AggregateId { get; }
		public PessoaFisica PessoaFisica { get; }
		public DateTime DataExecucao { get; }

		public PessoaFisicaCriada(Guid aggregateId, PessoaFisica pessoaFisica)
		{
			AggregateId = aggregateId;
			PessoaFisica = pessoaFisica;
			DataExecucao = DateTime.Now;
		}
	}
}
