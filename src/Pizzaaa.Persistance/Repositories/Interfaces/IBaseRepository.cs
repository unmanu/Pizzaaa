using Pizzaaa.Persistance.Models;

namespace Pizzaaa.Persistance.Repositories;

internal interface IBaseRepository<T> where T : BaseEntity
{
	Task<T?> FindById(int id);
	Task<List<T>> FindAll();

	Task Insert(T entity);

	Task<int> Delete(int id);

	Task<T?> Update(int id, Action<T> updateFields);

}
