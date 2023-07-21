namespace Pizzaaa.IntegrationTest.Mothers;

internal static class UserPizzaPreferenceMother
{
	internal static Persistance.Models.UserPizzaPreference AEntityUserPizzaPreference(int? id, Persistance.Models.Pizza pizza, Persistance.Models.User user)
	{
		return new()
		{
			ID = id ?? 0,
			Pizza = pizza,
			PizzaId = pizza.ID,
			User = user,
			UserId = user.ID,
			Favourite = true,
			Blacklisted = true,
			InsertUser = "integrationTest",
			InsertDate = new DateTime(2023, 3, 19, 16, 23, 30)
		};
	}
}
