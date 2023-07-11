using Pizzaaa.Persistance.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Models;

internal class Store
{
	public int StoreId { get; set; }

	[MaxLength(200)]
	public string? Name { get; set; }

	public List<Pizza> Pizzas { get; } = new();
}