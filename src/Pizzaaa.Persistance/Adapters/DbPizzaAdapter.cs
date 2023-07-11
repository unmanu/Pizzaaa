using Pizzaaa.BLL.Ports;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Adapters;

internal class DbPizzaAdapter : IPizzaPort
{
	private readonly PizzaRepository _pizzaRepository;

	public DbPizzaAdapter(PizzaRepository pizzaRepository)
	{
		this._pizzaRepository = pizzaRepository;
	}

	public async Task Insert(BLL.Models.Pizza pizza)
	{
		Models.Pizza entity = new()
		{
			Name = pizza.Name
		};
		await _pizzaRepository.Insert(entity);
	}
}