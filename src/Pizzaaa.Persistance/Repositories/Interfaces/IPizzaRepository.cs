using Pizzaaa.Persistance.Models;

namespace Pizzaaa.Persistance.Repositories.Interfaces;

internal interface IPizzaRepository : IBaseRepository<Pizza>
{
    Task<List<Pizza>> FindAllByStore(int storeId);
}
