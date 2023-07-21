using Pizzaaa.BLL.Models;

namespace Pizzaaa.Persistance.Test.Mothers;

public static class UserPizzaPreferenceMother
{
	public static UserPizzaPreference ABllUserPizzaPreference()
	{
		return new()
		{
			PizzaId = 901,
			UserId = 802
		};
	}
}
