using Microsoft.EntityFrameworkCore;
using Pizzaaa.BLL.Security;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;

namespace Pizzaaa.Persistance.Repositories;

internal class UserPizzaPreferenceRepository : BaseRepository<UserPizzaPreference>, IUserPizzaPreferenceRepository
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

