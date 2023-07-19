using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Ports;
using Pizzaaa.BLL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Services;

public class OrderService
{
	private readonly IOrderPort _orderPort;

	public OrderService(IOrderPort orderPort)
	{
		this._orderPort = orderPort;
    }

    public async Task<List<Order>> FindTodayOrders()
    {
        return await _orderPort.FindTodayOrders();
    }

    public async Task<Order> Insert(Order order)
    {
        return await _orderPort.Insert(order);
    }

    public async Task Delete(Order order)
    {
        await _orderPort.Delete(order);
    }
}
