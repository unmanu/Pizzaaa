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