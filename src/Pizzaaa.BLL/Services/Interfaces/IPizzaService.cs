using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Services.Interfaces;

public interface IPizzaService
{
    Task<List<Pizza>> FindAllByStore(int storeId);
}
