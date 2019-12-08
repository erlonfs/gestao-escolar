using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Demo.GestaoEscolar.Domain.ValueObjects;
using FluentAssertions;

namespace Demo.GestaoEscolar.Domain.Test.ValueObjects
{
	public class CpfTest
	{
		private Cpf _cpf;


		[Fact]
		public void Quando_criar_um_cpf()
		{
			var numero = "03443703135";

			_cpf = new Cpf(numero);

			_cpf.ToString().Should().Be(numero);

		}
	}
}
