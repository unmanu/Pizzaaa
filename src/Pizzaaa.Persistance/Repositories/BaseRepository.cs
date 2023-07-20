using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Pizzaaa.BLL.Security;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Repositories;

internal abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly PizzaContext _pizzaContext;
    protected readonly SecurityService _securityService;

	public BaseRepository(PizzaContext pizzaContext, SecurityService securityService)
	{
		this._pizzaContext = pizzaContext;
		this._securityService = securityService;
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
		if (entity is AuditedEntity auditedEntity) {
			AddInsertUser(auditedEntity);
		}

		await GetSet().AddAsync(entity);
		await _pizzaContext.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        T? toDelete = await FindById(entity.ID);
        if (toDelete != null)
        {
            GetSet().Remove(toDelete);
            await _pizzaContext.SaveChangesAsync();
        }
    }

    public async Task<T?> Update(T entity, Action<T> updateFields)
    {
        return await Update(entity.ID, updateFields);
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
		audit.InsertDate = DateTime.Now;
    }

    private void AddUpdateUser(AuditedEntity audit)
    {
        audit.UpdateUser = _securityService.GetLoggedUser().Username;
        audit.UpdateDate = DateTime.Now;
    }
}
