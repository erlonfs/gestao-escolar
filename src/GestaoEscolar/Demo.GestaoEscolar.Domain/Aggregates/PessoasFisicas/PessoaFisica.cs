using System;
using CrossCutting;
using Demo.GestaoEscolar.Domain.ValueObjects;

namespace Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas
{
	public class PessoaFisica : Aggregate<Guid>
	{
		public DateTime DataCriacao { get; private set; }

		public string Nome { get; private set; }		
		public virtual Cpf Cpf { get; private set; }
		public string NomeSocial { get; private set; }
		public string Sexo { get; private set; }
		public DateTime DataNascimento { get; private set; }

		public PessoaFisica(Guid id, string nome, string cpf, string nomeSocial, string sexo, DateTime dataNascimento)
		{
			EntityId = id;
			DataCriacao = DateTime.Now;

			Nome = nome;
			Cpf = new Cpf(cpf);
			NomeSocial = nomeSocial;
			Sexo = sexo;
			DataNascimento = dataNascimento;

			DomainEvents.Raise(new PessoaFisicaCriada(EntityId, this));

		}

		public void Alterar(string nome, string nomeSocial, string sexo, DateTime dataNascimento)
		{
			Nome = nome;
			NomeSocial = nomeSocial;
			Sexo = sexo;
			DataNascimento = dataNascimento;

			DomainEvents.Raise(new PessoaFisicaAlterada(EntityId, this));

		}

		public void AlterarCpf(string novoCpf)
		{
			Cpf = new Cpf(novoCpf);

			DomainEvents.Raise(new PessoaFisicaCpfAlterado(EntityId, this));

		}
	}
}
