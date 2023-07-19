﻿using Pizzaaa.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Ports;

public interface IPizzaPort
{
    Task<List<Pizza>> FindAllByStore(int storeId);

    Task Insert(Pizza pizza);
}
