﻿using Microsoft.EntityFrameworkCore;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder
        //	.Entity<PizzaEntity>()
        //	.HasOne(e => e.Store)
        //	.WithMany()
        //	.HasForeignKey(e => e.StoreId)
        //	.OnDelete(DeleteBehavior.NoAction);

        /*modelBuilder.Entity<PizzaEntity>()
			.HasMany(e => e.Ingredients)
			.WithMany(e => e.Pizzas)
			.UsingEntity(
			"IngredientsOnPizza",
			l => l.HasOne(typeof(PizzaEntity)).WithMany().HasForeignKey("PizzaId").HasPrincipalKey(nameof(PizzaEntity.PizzaId)),
			r => r.HasOne(typeof(IngredientEntity)).WithMany().HasForeignKey("IngredientId").HasPrincipalKey(nameof(IngredientEntity.IngredientId)),
			j => j.HasKey("PizzaId", "IngredientId"));*/
    }

}
