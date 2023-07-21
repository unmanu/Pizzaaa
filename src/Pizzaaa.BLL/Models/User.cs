namespace Pizzaaa.BLL.Models;

public class User
{
	public int ID { get; set; }

	public string Username { get; set; } = default!;

	public string Password { get; set; } = default!;

	public string Salt { get; set; } = default!;
}