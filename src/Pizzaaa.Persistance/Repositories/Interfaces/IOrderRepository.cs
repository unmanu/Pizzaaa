using Pizzaaa.Persistance.Models;

namespace Pizzaaa.Persistance.Repositories.Interfaces;

internal interface IOrderRepository : IBaseRepository<Order>
{
	Task<List<Order>> FindTodayOrders();
}
