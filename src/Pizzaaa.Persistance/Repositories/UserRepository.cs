using Microsoft.EntityFrameworkCore;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.System.Interfaces;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;

namespace Pizzaaa.Persistance.Repositories;

internal class UserRepository : BaseRepository<User>, IUserRepository
{

	public UserRepository(PizzaContext pizzaContext, ISecurityService securityService, IDateService dateService)
		: base(pizzaContext, securityService, dateService)
	{
	}

	protected override DbSet<User> GetSet()
	{
		return _pizzaContext.Users;
	}
}