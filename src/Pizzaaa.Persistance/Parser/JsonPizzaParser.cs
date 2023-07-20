using Pizzaaa.BLL.Utils;

namespace Pizzaaa.Persistance.Parser;

public static class JsonPizzaParser
{
    public static JsonPizzaContainer? ParseJsonPizza(string filePath)
    {
        return JsonUtils.DeserializeFile<JsonPizzaContainer>(filePath);
    }
}

public class JsonPizzaContainer
{
    public List<JsonPizza> Pizzas { get; set; } = new();
    public List<JsonIngredient> Ingredients { get; set; } = new();
}

public class JsonPizza
{
    public string? Shop { get; set; }
    public string? Name { get; set; }
    public List<string> Ingredients { get; set; } = new();
    public decimal? Price { get; set; }
}
public class JsonIngredient
{
    public string? Name { get; set; }
    public string? Type { get; set; }
}