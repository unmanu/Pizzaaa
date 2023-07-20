using Pizzaaa.BLL.Models;

namespace Pizzaaa.BLL.Ports;

public interface IIngredientPort
{
    Task<List<Ingredient>> FindAll();
}
