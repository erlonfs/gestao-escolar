using CrossCutting;
using System;
using System.Linq;
using System.Text.RegularExpressions;

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
			if (!Validar(numero)) throw new CpfInvalidoException();

			Numero = RemoverFormatacao(numero);

		}

		private string RemoverFormatacao(string value)
		{
			return Regex.Replace(value, @"[^0-9a-zA-Z]+", string.Empty);
		}

		private bool Validar(string numero)
		{
			if (string.IsNullOrWhiteSpace(numero)) throw new ArgumentNullException(nameof(numero));

			int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

			string tempCpf;
			string digito;
			int soma;
			int resto;
			var cpf = numero.Trim().Replace(".", "").Replace("-", "");

			if (cpf.Length != 11)
			{
				return false;
			}

			tempCpf = cpf.Substring(0, 9);

			var cpfArray = tempCpf.ToCharArray();
			if(cpfArray.All(x => x == cpfArray[0]))
			{
				return false;
			}

			soma = 0;

			for (int i = 0; i < 9; i++)
			{
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
			}

			resto = soma % 11;

			if (resto < 2)
			{
				resto = 0;
			}
			else
			{
				resto = 11 - resto;
			}

			digito = resto.ToString();
			tempCpf = tempCpf + digito;
			soma = 0;

			for (int i = 0; i < 10; i++)
			{
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
			}

			resto = soma % 11;

			if (resto < 2)
			{
				resto = 0;
			}
			else
			{
				resto = 11 - resto;
			}

			var digitoFinal = digito + resto.ToString();

			return cpf.EndsWith(digitoFinal);

		}

		public static implicit operator string(Cpf cpf)
		{
			return cpf.ToString();
		}

		public static implicit operator Cpf(string numero)
		{
			return new Cpf(numero);
		}

		public override string ToString()
		{
			return Numero.ToString();
		}

	}

	public class CpfInvalidoException : CrossCutting.ApplicationException
	{
		public CpfInvalidoException() : base("Cpf Invalido.") { }
	}
}
