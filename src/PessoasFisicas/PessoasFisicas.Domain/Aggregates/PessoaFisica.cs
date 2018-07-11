using PessoasFisicas.Domain.ValueObjects;
using SharedKernel.Common;
using System;

namespace PessoasFisicas.Domain.Aggregates
{
	public class PessoaFisica : Entity<Guid>
	{
		public int Id { get; private set; }
		public DateTime DataCriacao { get; private set; }
		public string Nome { get; private set; }
		public string Cpf { get; private set; }
		public string NomeSocial { get; private set; }
		public string Sexo { get; private set; }
		public DateTime DataNascimento { get; private set; }

		protected PessoaFisica()
		{

		}

		public PessoaFisica(Guid id, DateTime dataCriacao, string nome, string cpf,
							string nomeSocial, string sexo, DateTime dataNascimento)
		{
			EntityId = id;
			DataCriacao = dataCriacao;

			Nome = nome;
			Cpf = cpf;
			NomeSocial = nomeSocial;
			Sexo = sexo;
			DataNascimento = dataNascimento;

			DomainEvents.Raise(new PessoaFisicaCriada(EntityId, this));

		}

		public void Alterar(Guid id, string nome, string nomeSocial, string sexo, DateTime dataNascimento)
		{
			Nome = nome;
			NomeSocial = nomeSocial;
			Sexo = sexo;
			DataNascimento = dataNascimento;

			DomainEvents.Raise(new PessoaFisicaAlterada(EntityId, this));

		}

		public void AlterarCpf(string novoCpf)
		{
			Cpf = novoCpf;

			DomainEvents.Raise(new PessoaFisicaCpfAlterado(EntityId, this));

		}
	}
}
