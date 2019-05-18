using Dapper;
using Demo.GestaoEscolar.Domain.Finders;
using Demo.GestaoEscolar.Domain.Finders.Dtos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.Dapper.Data
{
	public class AlunoFinder : IAlunoFinder
	{
		private readonly AppConnectionString _appConnectionString;

		public AlunoFinder(AppConnectionString appConnectionString)
		{
			_appConnectionString = appConnectionString;
		}

		public async Task<IEnumerable<AlunoDto>> ObterAsync()
		{
			string sql = @"SELECT a.EntityId, a.DataCriacao, pf.EntityId AS PessoaFisicaId, 
						pf2.EntityId AS ResponsavelId, a.Matricula, a.SituacaoId, asi.Nome AS Situacao, 
						s.EntityId AS SalaId, e.Nome AS Escola, e.EntityId AS EscolaId, 
						CONCAT(s.FaseAno, ' - ', st.Nome) AS Sala 
						FROM GES.Aluno AS a
						INNER JOIN GES.AlunoSituacao AS asi ON asi.Id = a.SituacaoId
						INNER JOIN GES.PessoaFisica AS pf ON pf.Id = a.PessoaFisicaId
						INNER JOIN GES.PessoaFisica AS pf2 ON pf2.Id = a.ResponsavelId
						LEFT JOIN GES.SalaAluno AS sa ON sa.AlunoId = a.Id
						LEFT JOIN GES.Sala AS s ON s.Id = sa.SalaId
						LEFT JOIN GES.SalaTurno AS st ON st.Id = s.TurnoId
						LEFT JOIN GES.Escola AS e ON e.Id = s.EscolaId";

			using (var connection = new SqlConnection(_appConnectionString))
			{
				var alunos = await connection.QueryAsync<AlunoDto>(sql);
				if(alunos != null)
				{
					foreach (var aluno in alunos)
					{
						aluno.PessoaFisica = await ObterDadosPessoaFisicaPorIdAsync(aluno.PessoaFisicaId);
						aluno.Responsavel = await ObterDadosPessoaFisicaPorIdAsync(aluno.ResponsavelId);
					}

					return alunos;
				}
			}

			return null;
		}

		public async Task<PessoaFisicaDto> ObterDadosPessoaFisicaPorIdAsync(Guid pessoaFisicaId)
		{
			string sql = @"SELECT pf.Id, pf.EntityId, pf.DataCriacao, pf.Nome,
						 pf.Cpf, pf.NomeSocial, pf.Sexo, pf.DataNascimento 
						 FROM GES.PessoaFisica AS pf WHERE pf.EntityId = @PessoaFisicaId";

			using (var connection = new SqlConnection(_appConnectionString))
			{
				return await connection.QuerySingleOrDefaultAsync<PessoaFisicaDto>(sql, new { @PessoaFisicaId = pessoaFisicaId });
			}
		}
	}
}
