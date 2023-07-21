using Pizzaaa.BLL.Models;

namespace Pizzaaa.BLL.Ports;

public interface IUserPizzaPreferencePort
{
	Task<List<UserPizzaPreference>> FindAllByUser();
	Task<UserPizzaPreference> UpdateUserPreference(UserPizzaPreference userPizzaPreference);
}
