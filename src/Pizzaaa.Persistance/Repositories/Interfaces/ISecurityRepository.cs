using Pizzaaa.Persistance.Models;

namespace Pizzaaa.Persistance.Repositories.Interfaces;

internal interface ISecurityRepository
{

    Task<User?> FindByUsername(string username);

    Task Insert(User entity);

    Task<User?> Update(int id, Action<User> updateFields);
}
