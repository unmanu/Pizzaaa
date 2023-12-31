﻿using AutoMapper;
using Pizzaaa.BLL.Models.Exceptions;
using Pizzaaa.BLL.Ports;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;

namespace Pizzaaa.Persistance.Adapters;

internal class DbOrderAdapters : IOrderPort
{
	private readonly IOrderRepository _orderRepository;
	private readonly IPizzaRepository _pizzaRepository;
	private readonly IStoreRepository _storeRepository;
	private readonly IMapper _mapper;

	public DbOrderAdapters(
		IOrderRepository orderRepository,
		IPizzaRepository pizzaRepository,
		IStoreRepository storeRepository,
		IMapper mapper)
	{
		this._orderRepository = orderRepository;
		this._pizzaRepository = pizzaRepository;
		this._storeRepository = storeRepository;
		_mapper = mapper;

	}

	public async Task<List<BLL.Models.Order>> FindTodayOrders()
	{
		List<Order> orders = await _orderRepository.FindTodayOrders();
		return _mapper.Map<List<BLL.Models.Order>>(orders);
	}

	public async Task<BLL.Models.Order> Insert(BLL.Models.Order order)
	{
		if (order.PizzaId == 0)
		{
			throw new BllException("missing PizzaId");
		}
		if (order.StoreId == 0)
		{
			throw new BllException("missing StoreId");
		}
		if (string.IsNullOrWhiteSpace(order.OrderUser))
		{
			throw new BllException("missing OrderUser");
		}
		Order entity = _mapper.Map<Order>(order);
		Pizza? pizza = await _pizzaRepository.FindById(entity.PizzaId);
		Store? store = await _storeRepository.FindById(entity.StoreId);
		entity.Pizza = pizza!;
		entity.Store = store!;
		await _orderRepository.Insert(entity);
		return _mapper.Map<BLL.Models.Order>(entity);
	}

	public async Task Delete(BLL.Models.Order order)
	{
		await _orderRepository.Delete(order.ID);
	}
}