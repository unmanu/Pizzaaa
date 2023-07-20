using Microsoft.EntityFrameworkCore;
using Pizzaaa.BLL.Security;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;

namespace Pizzaaa.Persistance.Repositories;

internal class PizzaRepository : BaseRepository<Pizza>, IPizzaRepository
{

    public PizzaRepository(PizzaContext pizzaContext, SecurityService securityService)
        : base(pizzaContext, securityService)
    {
    }

    protected override DbSet<Pizza> GetSet()
    {
        return _pizzaContext.Pizzas;
    }


    public async Task<List<Pizza>> FindAllByStore(int storeId)
    {
        return await GetSet()
            .Include(x => x.Ingredients)
            .Where(x => x.StoreId == storeId)
            .ToListAsync();
    }
}
