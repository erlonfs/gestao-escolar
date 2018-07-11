using PessoasFisicas.Infra.Data;
using PessoasFisicas.Infra.Data.PessoaFisica;
using System;
using System.Threading.Tasks;

namespace PessoasFisicas.Infra.Dapper.Finders
{
	public class PessoaFisicaFinder : IPessoaFisicaFinder
	{
		public Task<PessoaFisicaDto> ObterAsync()
		{
			throw new NotImplementedException();
		}
	}
}
