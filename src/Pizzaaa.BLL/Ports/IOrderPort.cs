using Pizzaaa.BLL.Models;

namespace Pizzaaa.BLL.Ports;

public interface IOrderPort
{
    Task<List<Order>> FindTodayOrders();
    Task<Order> Insert(Order order);
    Task Delete(Order order);
}
