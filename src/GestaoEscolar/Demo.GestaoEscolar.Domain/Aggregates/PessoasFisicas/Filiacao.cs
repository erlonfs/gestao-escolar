namespace Demo.GestaoEscolar.Domain.Aggregates.PessoasFisicas
{
	public class Filiacao
    {
		public int PessoaFisicaId { get; private set; }
		public virtual PessoaFisica PessoaFisica { get; private set; }

		public int MaeId { get; private set; }
		public virtual PessoaFisica Mae { get; private set; }

		public int? PaiId { get; private set; }
		public virtual PessoaFisica Pai { get; private set; }
	}
}
