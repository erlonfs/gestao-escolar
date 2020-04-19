using Demo.GestaoEscolar.Domain.Aggregates.Alunos;

namespace Demo.GestaoEscolar.Domain.Aggregates.Escolas
{
	public class SalaAluno
	{
		public int Id { get; private set; }

		public int SalaId { get; private set; }
		public virtual Sala Sala { get; private set; }

		public int AlunoId { get; private set; }
		public virtual Aluno Aluno { get; private set; }

		protected SalaAluno()
		{

		}

		public SalaAluno(Sala sala, Aluno aluno)
		{
			Sala = sala;
			Aluno = aluno;
		}
	}
}
