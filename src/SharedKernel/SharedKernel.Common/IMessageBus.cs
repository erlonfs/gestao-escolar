using System.Threading.Tasks;

namespace SharedKernel.Common
{
	public interface IMessageBus
	{
		Task PublishAsync<T>(T e);
	}
}
