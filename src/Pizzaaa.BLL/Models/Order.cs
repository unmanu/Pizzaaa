namespace Pizzaaa.BLL.Models;

public class Order
{
	public int ID { get; set; }
	public int PizzaId { get; set; }
	public Pizza Pizza { get; set; } = new();
	public string OrderUser { get; set; } = default!;
	public int StoreId { get; set; }
	public Store Store { get; set; } = new();
	public DateOnly Date { get; set; }
}