using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Models;

internal class Ingredient : AuditedEntity
{

	[MaxLength(200)]
	public string Name { get; set; } = default!;

    [MaxLength(50)]
    public string Type { get; set; } = default!;

    public List<Pizza> Pizzas { get; } = new();
}