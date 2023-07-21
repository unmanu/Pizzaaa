using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Ports;
using Pizzaaa.BLL.Services.Interfaces;

namespace Pizzaaa.BLL.Services;

public class IngredientService : IIngredientService
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
