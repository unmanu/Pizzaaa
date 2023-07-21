using Microsoft.EntityFrameworkCore;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.System.Interfaces;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;

namespace Pizzaaa.Persistance.Repositories;

internal class OrderRepository : BaseRepository<Order>, IOrderRepository
{

	public OrderRepository(PizzaContext pizzaContext, ISecurityService securityService, IDateService dateService)
		: base(pizzaContext, securityService, dateService)
	{
	}

	protected override DbSet<Order> GetSet()
	{
		return _pizzaContext.Orders;
	}

	public async Task<List<Order>> FindTodayOrders()
	{
		DateOnly today = _dateService.GetTodayDateOnly();
		return await GetSet()
			.Include(x => x.Pizza)
			.Where(x => x.Date == today)
			.ToListAsync();
	}
}

