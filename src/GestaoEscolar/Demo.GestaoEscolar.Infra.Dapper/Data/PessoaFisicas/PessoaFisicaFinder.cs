using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.Dapper.Data.PessoasFisicas
{
    public class PessoaFisicaFinder : IPessoaFisicaFinder
	{
		private readonly AppConnectionString _appConnectionString;

		public PessoaFisicaFinder(AppConnectionString appConnectionString)
		{
			_appConnectionString = appConnectionString;
		}

		public async Task<IEnumerable<PessoaFisicaDto>> ObterAsync()
		{
			string sql = @"SELECT pf.Id, pf.EntityId, pf.DataCriacao, pf.Nome,
						 pf.Cpf, pf.NomeSocial, pf.Sexo, pf.DataNascimento 
						 FROM GES.PessoaFisica AS pf";

			using (var connection = new SqlConnection(_appConnectionString))
			{
				return await connection.QueryAsync<PessoaFisicaDto>(sql);
			}
		}
	}
}
