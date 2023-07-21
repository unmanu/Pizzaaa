using System.ComponentModel.DataAnnotations;

namespace Pizzaaa.Persistance.Models;

internal class Pizza : AuditedEntity
{

	[MaxLength(200)]
	public string Name { get; set; } = default!;

	public decimal? Price { get; set; }

	public List<Ingredient> Ingredients { get; } = new();

	public int StoreId { get; set; }
	public Store Store { get; set; } = new();


	public List<UserPizzaPreference> UserPizzaPreference { get; } = new();

}