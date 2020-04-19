namespace Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas
{
	public class Endereco
	{
		public int TipoLogradouroId { get; private set; }
		public virtual TipoLogradouro TipoLogradouro { get; private set; }

		public string Logradouro { get; private set; }
		public string Cep { get; private set; }
		public string Bairro { get; private set; }

	}

	public class TipoLogradouro { }

}


