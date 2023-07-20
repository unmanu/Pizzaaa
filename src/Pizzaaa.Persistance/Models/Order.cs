using System.ComponentModel.DataAnnotations;

namespace Pizzaaa.Persistance.Models;

internal class Order : AuditedEntity
{

    public int PizzaId { get; set; }
    public Pizza Pizza { get; set; } = new();
    [MaxLength(200)]
    public string OrderUser { get; set; } = default!;
    public int StoreId { get; set; }
    public Store Store { get; set; } = new();
    public DateOnly Date { get; set; }
}