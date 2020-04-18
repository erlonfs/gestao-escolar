using System.Threading.Tasks;

namespace CrossCutting
{
	public interface IHandler<T> where T : IDomainEvent
	{
		Task HandleAsync(T e);
	}
}
