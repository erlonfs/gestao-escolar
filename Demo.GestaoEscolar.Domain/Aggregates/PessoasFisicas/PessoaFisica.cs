using CrossCutting;
using Demo.GestaoEscolar.Agregates.PessoasFisicas;
using Demo.GestaoEscolar.Domain.ValueObjects;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Demo.GestaoEscolar.Domain.Test")]
namespace Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas
{
	public class PessoaFisica : Aggregate<Guid>
	{
		public int Id { get; private set; }
		public DateTime DataCriacao { get; private set; }

		public string Nome { get; private set; }
		public virtual Cpf Cpf { get; private set; }
		public string NomeSocial { get; private set; }
		public string Sexo { get; private set; }
		public DateTime DataNascimento { get; private set; }

		protected PessoaFisica()
		{

		}

		internal PessoaFisica(Guid id, string nome, string cpf, string nomeSocial, string sexo, DateTime dataNascimento)
		{
			EntityId = id;
			DataCriacao = DateTime.Now;

			Nome = nome;
			Cpf = new Cpf(cpf);
			NomeSocial = nomeSocial;
			Sexo = sexo;
			DataNascimento = dataNascimento;

			RaiseEvent(new PessoaFisicaCriada(EntityId, this));

		}

		internal void Alterar(string nome, string nomeSocial, string sexo, DateTime dataNascimento)
		{
			Nome = nome;
			NomeSocial = nomeSocial;
			Sexo = sexo;
			DataNascimento = dataNascimento;

			RaiseEvent(new PessoaFisicaAlterada(EntityId, this));

		}

		internal void AlterarCpf(string novoCpf)
		{
			Cpf = new Cpf(novoCpf);

			RaiseEvent(new PessoaFisicaCpfAlterado(EntityId, this));

		}
	}
}
