using AutoMapper;
using Pizzaaa.BLL.Ports;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
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
    private readonly StoreRepository _storeRepository;
    private readonly IMapper _mapper;

	public DbPizzaAdapter(PizzaRepository pizzaRepository, StoreRepository storeRepository, IMapper mapper)
	{
		this._pizzaRepository = pizzaRepository;
        this._storeRepository = storeRepository;
        _mapper = mapper;

	}

    public async Task<List<BLL.Models.Pizza>> FindAllByStore(int storeId)
    {
        List<Pizza> pizzas = await _pizzaRepository.FindAllByStore(storeId);

        return _mapper.Map<List<BLL.Models.Pizza>>(pizzas);
    }

    public async Task Insert(BLL.Models.Pizza pizza)
	{
		Models.Pizza entity = _mapper.Map<Models.Pizza>(pizza);
        Store? store = await _storeRepository.FindById(1);
		//entity.Store = store!;//TODO
        await _pizzaRepository.Insert(entity);
	}
}