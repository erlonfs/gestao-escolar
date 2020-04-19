namespace Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas
{
	public class Nacionalidade
	{
		public int PaisNacionalidadeId { get; private set; }
		public virtual Pais PaisNacionalidade { get; private set; }

		public int? EstadoNaturalidadeId { get; private set; }
		public virtual Estado EstadoNaturalidade { get; private set; }

	}

	public class Pais { }
	public class Estado { }

}


