using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Pizzaaa.BLL.Security;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Pizzaaa.Persistance.Repositories;

internal class PizzaRepository : BaseRepository<Pizza>
{

	public PizzaRepository(PizzaContext pizzaContext, SecurityService securityService)
		: base(pizzaContext, securityService)
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
