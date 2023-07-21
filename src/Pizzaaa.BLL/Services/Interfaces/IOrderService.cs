using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Services.Interfaces;

public interface IOrderService
{
    Task<List<Order>> FindTodayOrders();

    Task<Order> Insert(Order order);

    Task Delete(Order order);
}
