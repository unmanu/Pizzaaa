using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Ports;
using Pizzaaa.BLL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Services;

public class IngredientService
{
	private readonly IIngredientPort _ingredientPort;

	public IngredientService(IIngredientPort ingredientPort)
	{
		this._ingredientPort = ingredientPort;
    }

	public async Task<List<Ingredient>> FindAll()
	{
		return await _ingredientPort.FindAll();
    }
}
