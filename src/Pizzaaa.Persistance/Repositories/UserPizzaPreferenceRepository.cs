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

internal class UserPizzaPreferenceRepository : BaseRepository<UserPizzaPreference>
{

    public UserPizzaPreferenceRepository(PizzaContext pizzaContext, SecurityService securityService)
        : base(pizzaContext, securityService)
    {
    }

    protected override DbSet<UserPizzaPreference> GetSet()
    {
        return _pizzaContext.UserPizzaPreferences;
    }

    public async Task<List<UserPizzaPreference>> FindAllByUser()
    {
        int userId = _securityService.GetLoggedUser().ID;
        return await GetSet()
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }
}

