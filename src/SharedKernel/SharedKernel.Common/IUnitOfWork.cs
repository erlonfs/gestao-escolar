using System.Threading.Tasks;

namespace SharedKernel.Common
{
	public interface IUnitOfWork
	{
		Task CommitAsync();
		Task RollBackAsync();
	}
}
