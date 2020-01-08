using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossCutting
{
	public interface IRepository<T>
	{
		Task AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task RemoveAsync(T entity);
		Task<T> GetByEntityIdAsync(Guid entityId);
		Task<IEnumerable<T>> GetAllAsync();
	}
}
