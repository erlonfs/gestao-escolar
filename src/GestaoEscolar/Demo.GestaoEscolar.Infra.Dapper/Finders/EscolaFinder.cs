using Dapper;
using Demo.GestaoEscolar.Infra.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.Dapper.Finders
{
	public class EscolaFinder : IEscolaFinder
	{
		private readonly AppConnectionString _appConnectionString;

		public EscolaFinder(AppConnectionString appConnectionString)
		{
			_appConnectionString = appConnectionString;
		}

		public async Task<IEnumerable<EscolaDto>> ObterAsync()
		{
			string sql = @"SELECT e.Id, e.EntityId, e.DataCriacao, e.Nome 
						 FROM GES.Escola AS e";

			using (var connection = new SqlConnection(_appConnectionString))
			{
				var escolas = await connection.QueryAsync<EscolaDto>(sql);
				if(escolas != null)
				{
					foreach (var escola in escolas)
					{
						escola.Salas = await ObterSalasPorEscolaIdAsync(escola.EntityId);
					}

					return escolas;
				}
			}

			return null;
		}

		public async Task<IEnumerable<SalaDto>> ObterSalasPorEscolaIdAsync(Guid escolaId)
		{
			string sql = @"SELECT s.FaseAno, st.Nome AS Turno, 
						(SELECT COUNT(1) FROM GES.SalaAluno AS sa WHERE sa.SalaId = s.Id) AS  QtdAlunos
						 FROM GES.Sala AS s
						INNER JOIN GES.Escola AS e ON e.Id = s.EscolaId
						INNER JOIN GES.SalaTurno AS st ON st.Id = s.TurnoId
						WHERE e.EntityId = @EscolaId";

			using (var connection = new SqlConnection(_appConnectionString))
			{
				return await connection.QueryAsync<SalaDto>(sql, new { @EscolaId = escolaId });
			}
		}
	}
}
