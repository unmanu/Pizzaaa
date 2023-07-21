namespace Pizzaaa.IntegrationTest.Mothers;

internal static class UserMother
{
	internal static Persistance.Models.User AEntityUser(int? id, string username)
	{
		return new()
		{
			ID = id ?? 0,
			Username = username,
			Password = "pwd" + id,
			Salt = "salt" + id,
			InsertUser = "integrationTest",
			InsertDate = new DateTime(2023, 3, 19, 16, 23, 30)
		};
	}
}
