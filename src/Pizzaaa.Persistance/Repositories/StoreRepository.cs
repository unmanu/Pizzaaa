using Microsoft.EntityFrameworkCore;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Repositories;

internal class StoreRepository
{
	private readonly PizzaContext _pizzaContext;

	public StoreRepository(PizzaContext pizzaContext)
	{
		this._pizzaContext = pizzaContext;
	}

	public async Task<Store?> GetById(long id)
	{
		return await _pizzaContext.Stores.FirstOrDefaultAsync(x => x.StoreId == id);
	}

	public async Task Insert(Store store)
	{
		await _pizzaContext.Stores.AddAsync(store);
		await _pizzaContext.SaveChangesAsync();
	}
}
