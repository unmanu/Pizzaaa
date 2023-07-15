using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Models;

public class Pizza
{
	public int ID { get; set; }

	public string? Name { get; set; }

	//public List<Ingredient> Ingredients { get; } = new();

	//public Store Store { get; set; } = new();
}