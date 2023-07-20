using Microsoft.EntityFrameworkCore;
using Pizzaaa.BLL.Security;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Repositories;

internal class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
{
    public IngredientRepository(PizzaContext pizzaContext, SecurityService securityService)
    : base(pizzaContext, securityService)
    {
    }

    protected override DbSet<Ingredient> GetSet()
    {
        return _pizzaContext.Ingredients;
    }
}
