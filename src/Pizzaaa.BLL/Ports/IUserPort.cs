using Pizzaaa.BLL.Models;

namespace Pizzaaa.BLL.Ports;

public interface IUserPort
{
    Task<User?> FindByUsername(string username);
    Task<User> Insert(User user);
}
