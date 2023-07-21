using Pizzaaa.BLL.Models;

namespace Pizzaaa.BLL.Ports;

public interface IPizzaPort
{
	Task<List<Pizza>> FindAllByStore(int storeId);

}
