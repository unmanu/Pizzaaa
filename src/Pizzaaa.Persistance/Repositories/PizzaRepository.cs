using Microsoft.EntityFrameworkCore;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Repositories;

internal class PizzaRepository
{
	private readonly PizzaContext _pizzaContext;

	public PizzaRepository(PizzaContext pizzaContext)
	{
		this._pizzaContext = pizzaContext;
	}

	public async Task<Pizza?> GetById(long id)
	{
		return await _pizzaContext.Pizzas.FirstOrDefaultAsync(x => x.PizzaId == id);
	}

	public async Task Insert(Pizza pizza)
	{
		await _pizzaContext.Pizzas.AddAsync(pizza);
		await _pizzaContext.SaveChangesAsync();
	}
}
