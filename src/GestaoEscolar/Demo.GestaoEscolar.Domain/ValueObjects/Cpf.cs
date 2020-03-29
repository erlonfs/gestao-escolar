using CrossCutting;
using System;

namespace Demo.GestaoEscolar.Domain.ValueObjects
{
	public class Cpf : ValueObject<Cpf>
	{
		public string Numero { get; private set; }

		protected Cpf()
		{

		}

		public Cpf(string numero)
		{
			if (string.IsNullOrWhiteSpace(numero)) throw new ArgumentNullException(nameof(numero));

			if (Validar(numero))
			{
				Numero = numero;
			}
		}

		private bool Validar(string numero)
		{
			//TODO Regras de CPF

			return true;
		}

		public static implicit operator string(Cpf cpf)
		{
			return cpf.ToString();
		}

		public static explicit operator Cpf(string numero)
		{
			return new Cpf(numero);
		}

		public override string ToString()
		{
			return Numero.ToString();
		}

	}
}
