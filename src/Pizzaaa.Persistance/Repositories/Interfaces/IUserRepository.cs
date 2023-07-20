using Pizzaaa.Persistance.Models;

namespace Pizzaaa.Persistance.Repositories.Interfaces;

internal interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsername(string username);

    Task UpdateLastAccess(int id);
}
