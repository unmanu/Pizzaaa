using Pizzaaa.Persistance.Models;

namespace Pizzaaa.Persistance.Repositories.Interfaces;

internal interface IUserPizzaPreferenceRepository : IBaseRepository<UserPizzaPreference>
{
    Task<List<UserPizzaPreference>> FindAllByUser();
}
