namespace Pizzaaa.BLL.Models;

public class Ingredient
{
	public int ID { get; set; }

	public string Name { get; set; } = default!;
	public string Type { get; set; } = default!;

	public List<Pizza> Pizzas { get; } = new();
}