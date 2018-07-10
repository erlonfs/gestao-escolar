using System;
using System.Threading.Tasks;

namespace SharedKernel.Common
{
	public interface IRepository<T>
	{
		Task AddAsync(T entity);
		Task RemoveAsync(T entity);
		Task<T> GetByEntityIdAsync(Guid entityId);
	}
}
