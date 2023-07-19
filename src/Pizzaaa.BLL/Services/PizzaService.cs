using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Ports;
using Pizzaaa.BLL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Services;

public class PizzaService
{
	private readonly IPizzaPort _pizzaPort;
	private readonly SecurityService _securityService;
	private readonly IUserPort _userPort;

	public PizzaService(IPizzaPort pizzaPort, IUserPort userPort, SecurityService securityService)
	{
		this._pizzaPort = pizzaPort;
		this._userPort = userPort;
		this._securityService = securityService;
    }

	public async Task<List<Pizza>> FindAllByStore(int storeId)
	{
		return await _pizzaPort.FindAllByStore(storeId);
    }
}
