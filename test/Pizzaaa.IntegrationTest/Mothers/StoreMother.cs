namespace Pizzaaa.IntegrationTest.Mothers;

internal static class StoreMother
{
	internal static Persistance.Models.Store AEntityStore(int? id, string name)
	{
		return new()
		{
			ID = id ?? 0,
			Name = name,
			InsertUser = "integrationTest",
			InsertDate = new DateTime(2023, 3, 19, 16, 23, 30)
		};
	}
}
