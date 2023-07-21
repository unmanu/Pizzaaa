using Pizzaaa.BLL.Models;

namespace Pizzaaa.BLL.Ports;

public interface IStorePort
{
	Task<List<Store>> FindAll();
}
