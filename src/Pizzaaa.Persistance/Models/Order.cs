using Pizzaaa.BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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