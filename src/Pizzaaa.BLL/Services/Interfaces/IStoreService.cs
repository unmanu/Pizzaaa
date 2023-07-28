using Pizzaaa.BLL.Models;

namespace Pizzaaa.BLL.Services.Interfaces;

public interface IStoreService
{
	Task<List<Store>> FindAll();
}
