using Microsoft.EntityFrameworkCore;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.System.Interfaces;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;

namespace Pizzaaa.Persistance.Repositories;

internal abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
	protected readonly PizzaContext _pizzaContext;
	protected readonly ISecurityService _securityService;
	protected readonly IDateService _dateService;

	protected BaseRepository(PizzaContext pizzaContext, ISecurityService securityService, IDateService dateService)
	{
		this._pizzaContext = pizzaContext;
		this._securityService = securityService;
		this._dateService = dateService;
	}

	protected abstract DbSet<T> GetSet();

	public async Task<T?> FindById(int id)
	{
		return await GetSet().FirstOrDefaultAsync(x => x.ID == id);
	}

	public async Task<List<T>> FindAll()
	{
		return await GetSet().ToListAsync();
	}

	public async Task Insert(T entity)
	{
		if (entity is AuditedEntity auditedEntity)
		{
			AddInsertUser(auditedEntity);
		}

		await GetSet().AddAsync(entity);
		await _pizzaContext.SaveChangesAsync();
	}

	public async Task<int> Delete(int id)
	{
		T? toDelete = await FindById(id);
		if (toDelete != null)
		{
			GetSet().Remove(toDelete);
			return await _pizzaContext.SaveChangesAsync();
		}
		return 0;
	}

	public async Task<T?> Update(int id, Action<T> updateFields)
	{
		T? toBeUpdated = await FindById(id);
		if (toBeUpdated == null)
		{
			return null;
		}
		updateFields(toBeUpdated);
		if (toBeUpdated is AuditedEntity auditedEntity)
		{
			AddUpdateUser(auditedEntity);
		}
		await _pizzaContext.SaveChangesAsync();
		return toBeUpdated;
	}

	private void AddInsertUser(AuditedEntity audit)
	{
		audit.InsertUser = _securityService.GetLoggedUser().Username;
		audit.InsertDate = _dateService.GetNow();
	}

	private void AddUpdateUser(AuditedEntity audit)
	{
		audit.UpdateUser = _securityService.GetLoggedUser().Username;
		audit.UpdateDate = _dateService.GetNow();

	}
}
