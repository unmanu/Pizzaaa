using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Parser;

namespace Pizzaaa.Persistance.Data;


internal static class DbInitializer
{
	private const string SYSTEM_USER = "system";
	private static DateTime _insertDate;

	public static void Initialize(PizzaContext context, string? jsonSourceInitializert = null)
	{
		if (context.Ingredients.Any())
		{
			return;   // DB has been seeded
		}
		_insertDate = DateTime.Now;

		if (!string.IsNullOrWhiteSpace(jsonSourceInitializert) && File.Exists(jsonSourceInitializert))
		{
			JsonPizzaContainer? container = JsonPizzaParser.ParseJsonPizza(jsonSourceInitializert);
			if (container != null)
			{
				InsertJsonData(context, container);
			}
		}
	}

	private static void InsertJsonData(PizzaContext context, JsonPizzaContainer container)
	{
		InsertJsonIngredients(context, container.Ingredients);
		InsertJsonStores(context, container.Pizzas);
		InsertJsonPizzas(context, container.Pizzas);
	}

	private static void InsertJsonIngredients(PizzaContext context, List<JsonIngredient> ingredients)
	{
		foreach (JsonIngredient jsonIngredient in ingredients)
		{
			if (string.IsNullOrEmpty(jsonIngredient.Name))
			{
				continue;
			}
			Ingredient ingredient = new()
			{
				Name = jsonIngredient.Name,
				Type = jsonIngredient.Type ?? "",
				InsertUser = SYSTEM_USER,
				InsertDate = _insertDate,
			};
			context.Ingredients.Add(ingredient);
		}
		context.SaveChanges();
	}

	private static void InsertJsonStores(PizzaContext context, List<JsonPizza> pizzas)
	{
		foreach (string? storeName in pizzas.Select(x => x.Shop).Distinct())
		{
			if (string.IsNullOrEmpty(storeName))
			{
				continue;
			}
			Store store = new()
			{
				Name = storeName,
				InsertUser = SYSTEM_USER,
				InsertDate = _insertDate,
			};
			context.Stores.Add(store);
		}
		context.SaveChanges();
	}

	private static void InsertJsonPizzas(PizzaContext context, List<JsonPizza> pizzas)
	{
		List<Store> stores = context.Stores.ToList();
		List<Ingredient> ingredients = context.Ingredients.ToList();

		foreach (JsonPizza? jsonPizza in pizzas)
		{
			if (string.IsNullOrEmpty(jsonPizza.Name))
			{
				continue;
			}
			Store? store = stores.FirstOrDefault(x => x.Name == jsonPizza.Shop);
			if (store == null)
			{
				continue;
			}
			List<Ingredient> foundIngredients = ingredients.Where(x => jsonPizza.Ingredients.Contains(x.Name)).ToList();
			Pizza pizza = new()
			{
				Name = jsonPizza.Name,
				Price = jsonPizza.Price,
				Store = store,
				InsertUser = SYSTEM_USER,
				InsertDate = _insertDate,
			};
			pizza.Ingredients.AddRange(foundIngredients);
			context.Pizzas.Add(pizza);
		}
		context.SaveChanges();
	}
}

