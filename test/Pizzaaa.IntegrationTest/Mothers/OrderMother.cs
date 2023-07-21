using Pizzaaa.Persistance.Models;

namespace Pizzaaa.IntegrationTest.Mothers;

internal static class OrderMother
{
	internal static Persistance.Models.Order AEntityOrder(int? id, DateOnly date, string orderUser, Pizza pizza, Store store)
	{
		return new()
		{
			ID = id ?? 0,
			Date = date,
			OrderUser = orderUser,
			Pizza = pizza,
			PizzaId = pizza.ID,
			Store = store,
			StoreId = store.ID,
			InsertUser = "integrationTest",
			InsertDate = new DateTime(2023, 3, 19, 16, 23, 30)
		};
	}
}
