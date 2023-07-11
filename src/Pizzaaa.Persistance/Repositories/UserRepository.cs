using Microsoft.EntityFrameworkCore;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Repositories;

internal class UserRepository
{
	private readonly PizzaContext _pizzaContext;

	public UserRepository(PizzaContext pizzaContext)
	{
		this._pizzaContext = pizzaContext;
	}

	public async Task<User?> GetById(long id)
	{
		return await _pizzaContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
	}

	public async Task Insert(User user)
	{
		await _pizzaContext.Users.AddAsync(user);
		await _pizzaContext.SaveChangesAsync();
	}
}
