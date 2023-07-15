using Pizzaaa.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Ports;

public interface IUserPort
{
    Task<User?> FindByUsername(string username);
    Task<User> Insert(User user);

    Task UpdateLastAccess(int id);
}
