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

    public decimal? Price { get; set; }

    public string Name { get; set; } = default!;

    public List<Ingredient> Ingredients { get; } = new();


    public string GetIngredientsList()
    {
        if (Ingredients == null || !Ingredients.Any())
        {
            return "";
        }
        return Ingredients.Select(x => x.Name).Aggregate((i, j) => i + ", " + j);
    }
    
}