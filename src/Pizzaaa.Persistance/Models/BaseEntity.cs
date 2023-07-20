using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzaaa.Persistance.Models;

internal abstract class BaseEntity
{
    [Column(Order = 0)]
    public int ID { get; set; }

}