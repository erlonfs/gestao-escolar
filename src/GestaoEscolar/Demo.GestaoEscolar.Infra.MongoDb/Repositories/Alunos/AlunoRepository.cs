using Demo.GestaoEscolar.Domain.Aggregates.Alunos;
using Demo.GestaoEscolar.Domain.Repositories.Alunos;
using MongoDB.Driver;

namespace Demo.GestaoEscolar.Infra.MongoDb.Repositories.Alunos
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
	{
		public AlunoRepository(IMongoContext context) : base(context)
		{
			
		}
	}
}
