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

internal class UserRepository : BaseRepository<User>
{

    public UserRepository(PizzaContext pizzaContext, SecurityService securityService)
        : base(pizzaContext, securityService)
    {
    }

    protected override DbSet<User> GetSet()
    {
        return _pizzaContext.Users;
    }

    public async Task<User?> FindByUsername(string username)
    {
        return await GetSet().FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task UpdateLastAccess(int id)
    {
        await Update(id, x => x.LastAccess = DateTime.Now);
    }
}