using Pizzaaa.BLL.Models;

namespace Pizzaaa.BLL.Services.Interfaces;

public interface IIngredientService
{
	Task<List<Ingredient>> FindAll();
}
