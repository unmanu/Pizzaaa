using Pizzaaa.BLL.Models;

namespace Pizzaaa.BLL.Services.Interfaces;

public interface IOrderService
{
	Task<List<Order>> FindTodayOrders();

	Task<Order> Insert(Order order);

	Task Delete(Order order);
}
