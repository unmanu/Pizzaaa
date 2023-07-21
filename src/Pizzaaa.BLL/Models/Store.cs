namespace Pizzaaa.BLL.Models;

public class Store
{
	public int ID { get; set; }

	public string Name { get; set; } = default!;

	public List<Pizza> Pizzas { get; } = new();
}