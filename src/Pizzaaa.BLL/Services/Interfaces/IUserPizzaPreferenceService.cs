using Pizzaaa.BLL.Models;

namespace Pizzaaa.BLL.Services.Interfaces;

public interface IUserPizzaPreferenceService
{
	Task<List<UserPizzaPreference>> FindAllByUser();

	Task<UserPizzaPreference> UpdateUserPreference(UserPizzaPreference userPizzaPreference);
}
