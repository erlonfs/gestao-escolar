using System;
using System.Threading.Tasks;

namespace Demo.GestaoEscolar.Domain.Services.Alunos
{
	public class MatriculaService : IMatriculaService
	{
		public Task<int> GerarMatriculaAsync()
		{
			return Task.FromResult(new Random().Next(1000, 9999));
		}
	}
}
