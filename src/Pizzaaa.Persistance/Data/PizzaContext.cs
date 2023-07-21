using Microsoft.EntityFrameworkCore;
using Pizzaaa.Persistance.Models;

namespace Pizzaaa.Persistance.Data;

internal class PizzaContext : DbContext
{
	public PizzaContext(DbContextOptions<PizzaContext> options)
		: base(options)
	{
	}

	public DbSet<Store> Stores { get; set; }
	public DbSet<Pizza> Pizzas { get; set; }
	public DbSet<Ingredient> Ingredients { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<UserPizzaPreference> UserPizzaPreferences { get; set; }
	public DbSet<Order> Orders { get; set; }

}
