using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Ports;
using Pizzaaa.BLL.Services.Interfaces;

namespace Pizzaaa.BLL.Services;

public class StoreService : IStoreService
{
	private readonly IStorePort _storePort;

	public StoreService(IStorePort storePort)
	{
		this._storePort = storePort;
	}

	public async Task<List<Store>> FindAll()
	{
		return await _storePort.FindAll();
	}
}