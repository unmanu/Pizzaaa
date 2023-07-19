using Microsoft.EntityFrameworkCore;
using Pizzaaa.BLL.Security;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Repositories;

internal class SecurityRepository
{
    protected readonly PizzaContext _pizzaContext;

    public SecurityRepository(PizzaContext pizzaContext)
    {
        this._pizzaContext = pizzaContext;
    }

    public async Task<User?> FindByUsername(string username)
    {
        return await _pizzaContext.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task Insert(User entity)
    {
        entity.InsertUser = entity.Username;
        entity.InsertDate = DateTime.Now;
        await _pizzaContext.Users.AddAsync(entity);
        await _pizzaContext.SaveChangesAsync();
    }


    public async Task<User?> Update(int id, Action<User> updateFields)
    {
        User? toBeUpdated = await _pizzaContext.Users.FirstOrDefaultAsync(x => x.ID == id);
        if (toBeUpdated == null)
        {
            return null;
        }
        updateFields(toBeUpdated);
        toBeUpdated.UpdateUser = toBeUpdated.Username;
        toBeUpdated.UpdateDate = DateTime.Now;
        await _pizzaContext.SaveChangesAsync();
        return toBeUpdated;
    }

}