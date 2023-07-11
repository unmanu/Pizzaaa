using Pizzaaa.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Data;


internal static class DbInitializer
{
	public static void Initialize(PizzaContext context)
	{
		if (context.Ingredients.Any())
		{
			return;   // DB has been seeded
		}


		InsertTest(context);
	}


	private static void InsertTest(PizzaContext context)
	{
		Ingredient ingredient = new()
		{
			Name = "Fagiolo"
		};
		context.Ingredients.Add(ingredient);
		context.SaveChanges();

		User user = new()
		{
			Name = "Bombadil"
		};
		context.Users.Add(user);
		context.SaveChanges();

		Store store = new()
		{
			Name = "Ciacco"
		};
		context.Stores.Add(store);
		context.SaveChanges();

		Models.Pizza pizza = new()
		{
			Name = "Milla",
			Ingredients = { ingredient },
			Store = store
		};
		context.Pizzas.Add(pizza);
		context.SaveChanges();
	}
}

