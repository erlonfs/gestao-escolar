using System.Threading.Tasks;

namespace CrossCutting
{
	public interface IMessageBus
	{
		Task PublishAsync<T>(T e);
		Task<bool> IsAliveAsync();
	}
}
