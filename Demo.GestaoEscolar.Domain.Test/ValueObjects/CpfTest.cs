using Demo.GestaoEscolar.Domain.ValueObjects;
using FluentAssertions;
using System;
using Xunit;

namespace Demo.GestaoEscolar.Domain.Test.ValueObjects
{
	public class CpfTest
	{
		[Theory]
		[InlineData("20782878300", "20782878300")]
		[InlineData("207.82878300", "20782878300")]
		[InlineData("207.828.78300", "20782878300")]
		[InlineData("207.828.783-00", "20782878300")]
		[InlineData("207828783-00", "20782878300")]
		[InlineData("207.828783-00", "20782878300")]
		public void criar_um_cpf__com_numero_valido__deve_constar_o_mesmo(string entrada, string numero)
		{
			var cpf = new Cpf(entrada);

			cpf.Should().Be(new Cpf(numero));

		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("	")]
		public void criar_um_cpf__com_numero_nulo_ou_vazio__deve_lancar_erro(string numero)
		{
			Action act = () => { new Cpf(numero); };

			act.Should().Throw<ArgumentNullException>();

		}

		[Theory]
		[InlineData("0")]
		[InlineData("01")]
		[InlineData("012")]
		[InlineData("0123")]
		[InlineData("01234")]
		[InlineData("012345")]
		[InlineData("0123456")]
		[InlineData("01234567")]
		[InlineData("012345678")]
		[InlineData("0123456789")]
		[InlineData("01234567899")]
		[InlineData("00000000000")]
		[InlineData("11111111111")]
		[InlineData("22222222222")]
		[InlineData("33333333333")]
		[InlineData("44444444444")]
		[InlineData("55555555555")]
		[InlineData("66666666666")]
		[InlineData("77777777777")]
		[InlineData("88888888888")]
		[InlineData("99999999999")]
		[InlineData("012345678999")]
		[InlineData("0123456789994")]
		public void criar_um_cpf__com_numero_invalido__deve_lancar_erro(string numero)
		{
			Action act = () => { new Cpf(numero); };

			act.Should().Throw<CpfInvalidoException>();

		}
	}
}

