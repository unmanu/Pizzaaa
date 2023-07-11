using Pizzaaa.Persistance.Models;

namespace Pizzaaa.Persistance.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Ingredient ingredient = new()
        {
            Name = "salame"
        };


        Assert.NotNull(ingredient);
    }
}