﻿using Pizzaaa.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Ports;

public interface IOrderPort
{
    Task<List<Order>> FindTodayOrders();
    Task<Order> Insert(Order order);
    Task Delete(Order order);
}