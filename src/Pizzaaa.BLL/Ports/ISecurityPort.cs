using Pizzaaa.BLL.Models;

namespace Pizzaaa.BLL.Ports;

public interface ISecurityPort
{
	Task<User?> FindByUsername(string username);
	Task<User> Insert(User user);
	Task UpdateLastAccess(int id);
}
