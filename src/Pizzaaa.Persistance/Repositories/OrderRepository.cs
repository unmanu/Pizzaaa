using Microsoft.EntityFrameworkCore;
using Pizzaaa.BLL.Security;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;

namespace Pizzaaa.Persistance.Repositories;

internal class OrderRepository : BaseRepository<Order>
{

    public OrderRepository(PizzaContext pizzaContext, SecurityService securityService)
        : base(pizzaContext, securityService)
    {
    }

    protected override DbSet<Order> GetSet()
    {
        return _pizzaContext.Orders;
    }

    public async Task<List<Order>> FindTodayOrders()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        return await GetSet()
            .Include(x => x.Pizza)
            .Where(x => x.Date == today)
            .ToListAsync();
    }
}

