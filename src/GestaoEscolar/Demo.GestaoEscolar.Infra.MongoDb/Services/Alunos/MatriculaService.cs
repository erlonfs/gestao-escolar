using Demo.GestaoEscolar.Domain.Services.Alunos;
using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Infra.MongoDb.Services.Alunos
{
	public class MatriculaService : IMatriculaService
	{
		public Task<int> GerarMatriculaAsync()
		{
			return Task.FromResult(new Random().Next(1000, 9999));
		}
	}
}
