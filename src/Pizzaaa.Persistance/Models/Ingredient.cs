using System.ComponentModel.DataAnnotations;

namespace Pizzaaa.Persistance.Models;

internal class Ingredient : AuditedEntity
{

    [MaxLength(200)]
    public string Name { get; set; } = default!;

    [MaxLength(50)]
    public string Type { get; set; } = default!;

    public List<Pizza> Pizzas { get; } = new();
}