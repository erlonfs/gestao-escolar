using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossCutting.ValueObjects
{
	[NotMapped]
	public class Ano : ValueObject<Ano>
	{
		public int Numero { get; private set; }

		protected Ano()
		{

		}

		public Ano(int numero)
		{
			if (numero.ToString().Length > 4) throw new ArgumentOutOfRangeException(nameof(numero));
			Numero = numero;
		}
	}
}
