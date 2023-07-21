using Microsoft.EntityFrameworkCore;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.System.Interfaces;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;

namespace Pizzaaa.Persistance.Repositories;

internal class PizzaRepository : BaseRepository<Pizza>, IPizzaRepository
{

	public PizzaRepository(PizzaContext pizzaContext, ISecurityService securityService, IDateService dateService)
		: base(pizzaContext, securityService, dateService)
	{
	}

	protected override DbSet<Pizza> GetSet()
	{
		return _pizzaContext.Pizzas;
	}


	public async Task<List<Pizza>> FindAllByStore(int storeId)
	{
		return await GetSet()
			.Include(x => x.Ingredients)
			.Where(x => x.StoreId == storeId)
			.ToListAsync();
	}
}
