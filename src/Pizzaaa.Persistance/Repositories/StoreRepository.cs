using Microsoft.EntityFrameworkCore;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.System.Interfaces;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;

namespace Pizzaaa.Persistance.Repositories;

internal class StoreRepository : BaseRepository<Store>, IStoreRepository
{

	public StoreRepository(PizzaContext pizzaContext, ISecurityService securityService, IDateService dateService)
		: base(pizzaContext, securityService, dateService)
	{
	}

	protected override DbSet<Store> GetSet()
	{
		return _pizzaContext.Stores;
	}
}
