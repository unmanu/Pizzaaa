using Pizzaaa.BLL.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Services;

public class PizzaService
{
	private readonly IPizzaPort _pizzaPort;

	public PizzaService(IPizzaPort pizzaPort)
	{
		this._pizzaPort = pizzaPort;
	}

	public async Task Do()
	{
		await _pizzaPort.Insert(new ()
		{
			Name = "Pizzone"
		});
    }
}
