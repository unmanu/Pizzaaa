﻿using AutoMapper;
using Pizzaaa.BLL.Ports;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories;

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

    public async Task<List<BLL.Models.Pizza>> FindAllByStore(int storeId)
    {
        List<Pizza> pizzas = await _pizzaRepository.FindAllByStore(storeId);

        return _mapper.Map<List<BLL.Models.Pizza>>(pizzas);
    }
}