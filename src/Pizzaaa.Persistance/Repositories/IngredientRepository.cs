using Microsoft.EntityFrameworkCore;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Repositories;

internal class IngredientRepository
{
	private readonly PizzaContext _pizzaContext;

	public IngredientRepository(PizzaContext pizzaContext)
	{
		this._pizzaContext = pizzaContext;
	}

	public async Task<Ingredient?> GetById(long id)
	{
		return await _pizzaContext.Ingredients.FirstOrDefaultAsync(x => x.IngredientId == id);
	}

	public async Task Insert(Ingredient ingredient)
	{
		await _pizzaContext.Ingredients.AddAsync(ingredient);
		await _pizzaContext.SaveChangesAsync();
	}
}
