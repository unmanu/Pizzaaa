using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Models;

public class Ingredient 
{
    public int ID { get; set; }

    public string Name { get; set; } = default!;
    public string Type { get; set; } = default!;

    public List<Pizza> Pizzas { get; } = new();
}