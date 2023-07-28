using Pizzaaa.BLL.Models;

namespace Pizzaaa.BLL.Services.Interfaces;

public interface IPizzaService
{
	Task<List<Pizza>> FindAllByStore(int storeId);
}
