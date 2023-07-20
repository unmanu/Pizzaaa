namespace Pizzaaa.BLL.Models;

public class UserPizzaPreference
{
    public int ID { get; set; }
    public int PizzaId { get; set; }
    public Pizza Pizza { get; set; } = new();
    public int UserId { get; set; }
    public User User { get; set; } = new();
    public bool Blacklisted { get; set; }
    public bool Favourite { get; set; }
}