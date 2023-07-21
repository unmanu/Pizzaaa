using Pizzaaa.Persistance.Models;

namespace Pizzaaa.IntegrationTest.Mothers;

internal static class PizzaMother
{
	internal static Persistance.Models.Pizza AEntityPizza(int? id, string name, Store store, List<Ingredient>? ingredients = null)
	{
		Persistance.Models.Pizza pizza = new()
		{
			ID = id ?? 0,
			Name = name,
			Price = 9,
			Store = store,
			StoreId = store.ID,
			InsertUser = "integrationTest",
			InsertDate = new DateTime(2023, 3, 19, 16, 23, 30)
		};
		if (ingredients != null && ingredients.Any())
		{
			pizza.Ingredients.AddRange(ingredients);
		}
		return pizza;

	}
}
