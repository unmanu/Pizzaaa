using AutoMapper;
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
	private readonly IMapper _mapper;

	public DbPizzaAdapter(PizzaRepository pizzaRepository, IMapper mapper)
	{
		this._pizzaRepository = pizzaRepository;
		_mapper = mapper;

	}

	public async Task Insert(BLL.Models.Pizza pizza)
	{
		Models.Pizza entity = _mapper.Map<Models.Pizza>(pizza);
		await _pizzaRepository.Insert(entity);
	}
}