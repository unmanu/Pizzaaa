using System.ComponentModel.DataAnnotations;

namespace Pizzaaa.Persistance.Models;

internal class Store : AuditedEntity
{

	[MaxLength(200)]
	public string Name { get; set; } = default!;

	public List<Pizza> Pizzas { get; } = new();
}