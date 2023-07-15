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
			Name = "Fagiolo",
            InsertUser = "system"
        };
		context.Ingredients.Add(ingredient);
		context.SaveChanges();

		User user = new()
		{
			Username = "Bombadil",
			Password = "password",
			Salt = "Salt",
            InsertUser = "system"
        };
		context.Users.Add(user);
		context.SaveChanges();

		Store store = new()
		{
			Name = "Ciacco",
            InsertUser = "system"
        };
		context.Stores.Add(store);
		context.SaveChanges();

		Pizza pizza = new()
		{
			Name = "Milla",
			Ingredients = { ingredient },
			Store = store,
			InsertUser = "system"
		};
		context.Pizzas.Add(pizza);
		context.SaveChanges();
	}
}

