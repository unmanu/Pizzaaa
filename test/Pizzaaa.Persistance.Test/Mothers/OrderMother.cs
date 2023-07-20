namespace Pizzaaa.Persistance.Test.Mothers;

public static class OrderMother
{

    public static BLL.Models.Order ABllOrder()
    {
        return new()
        {
            OrderUser = "jack",
            Date = new DateOnly(2023, 02, 19),
            PizzaId = 105,
            StoreId = 206
        };
    }
}
