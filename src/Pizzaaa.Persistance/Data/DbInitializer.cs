using Microsoft.IdentityModel.Tokens;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Data;


internal static class DbInitializer
{
    private const string SYSTEM_USER = "system";
    private static DateTime _insertDate;

    public static void Initialize(PizzaContext context, string? jsonSourceInitializert)
    {
        if (context.Ingredients.Any())
        {
            return;   // DB has been seeded
        }
        _insertDate = DateTime.Now;

        if (jsonSourceInitializert != null)
        {
            JsonPizzaContainer? container = JsonPizzaParser.ParseJsonPizza(jsonSourceInitializert);
            if (container != null)
            {
                InsertJsonData(context, container);
                return;
            }
        }

        InsertSamples(context);
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
            if(store == null)
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

    private static void InsertSamples(PizzaContext context)
    {
        Ingredient ingredient = new()
        {
            Name = "Fagiolo",
            Type = "a",
            InsertUser = SYSTEM_USER
        };
        context.Ingredients.Add(ingredient);
        context.SaveChanges();

        User user = new()
        {
            Username = "Bombadil",
            Password = "password",
            Salt = "Salt",
            InsertUser = SYSTEM_USER
        };
        context.Users.Add(user);
        context.SaveChanges();

        Store ciacco = new()
        {
            Name = "Ciacco",
            InsertUser = SYSTEM_USER
        };
        context.Stores.Add(ciacco);
        Store canton = new()
        {
            Name = "Canton",
            InsertUser = SYSTEM_USER
        };
        context.Stores.Add(canton);
        context.SaveChanges();

        Pizza pizza = new()
        {
            Name = "Milla",
            Ingredients = { ingredient },
            Store = ciacco,
            InsertUser = SYSTEM_USER
        };
        context.Pizzas.Add(pizza);
        context.SaveChanges();
    }
}

