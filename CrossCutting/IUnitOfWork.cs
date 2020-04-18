using System.Threading.Tasks;

namespace CrossCutting
{
	public interface IUnitOfWork
	{
		Task CommitAsync();
		Task RollBackAsync();
	}
}
