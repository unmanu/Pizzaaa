using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Ports;
using Pizzaaa.BLL.Services.Interfaces;

namespace Pizzaaa.BLL.Services;

public class PizzaService : IPizzaService
{
	private readonly IPizzaPort _pizzaPort;

	public PizzaService(IPizzaPort pizzaPort)
	{
		this._pizzaPort = pizzaPort;
	}

	public async Task<List<Pizza>> FindAllByStore(int storeId)
	{
		return await _pizzaPort.FindAllByStore(storeId);
	}
}
