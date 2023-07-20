using Microsoft.EntityFrameworkCore;
using Pizzaaa.BLL.Security;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;

namespace Pizzaaa.Persistance.Repositories;

internal class StoreRepository : BaseRepository<Store>
{

    public StoreRepository(PizzaContext pizzaContext, SecurityService securityService)
        : base(pizzaContext, securityService)
    {
    }

    protected override DbSet<Store> GetSet()
    {
        return _pizzaContext.Stores;
    }
}
