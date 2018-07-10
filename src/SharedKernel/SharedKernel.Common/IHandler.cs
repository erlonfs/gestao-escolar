using System.Threading.Tasks;

namespace SharedKernel.Common
{
	public interface IHandler<T> where T : IDomainEvent
	{
		Task HandleAsync(T e);
	}
}
