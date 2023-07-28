using Moq;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.System.Interfaces;
using Pizzaaa.IntegrationTest.Mothers;
using Pizzaaa.IntegrationTest.Setup;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories;

namespace Pizzaaa.IntegrationTest.Repositories;

public class UserPizzaPreferenceRepositoryIntegrationTest : BaseDatabaseTest
{
	private readonly Mock<ISecurityService> _mockSecurityService;
	private readonly Mock<IDateService> _mockDateService;

	public UserPizzaPreferenceRepositoryIntegrationTest()
	{
		_mockSecurityService = new Mock<ISecurityService>();
		_mockDateService = new Mock<IDateService>();
	}

	[Fact]
	public async Task FindAllByUser_FoundsPreferences_ReturnsUserPreferences()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		UserPizzaPreferenceRepository repository = new(context, _mockSecurityService.Object, _mockDateService.Object);

		_mockSecurityService.Setup(mock => mock.GetLoggedId()).Returns(18);

		List<UserPizzaPreference> result = await repository.FindAllByUser();

		Assert.NotNull(result);
		Assert.Single(result);
	}

	[Fact]
	public async Task FindAllByUser_NoPreferencesFound_ReturnsEmptyList()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		UserPizzaPreferenceRepository repository = new(context, _mockSecurityService.Object, _mockDateService.Object);

		_mockSecurityService.Setup(mock => mock.GetLoggedId()).Returns(9999);

		List<UserPizzaPreference> result = await repository.FindAllByUser();

		Assert.NotNull(result);
		Assert.Empty(result);
	}


	private async Task DbTestInitializer(PizzaContext context)
	{
		Store pizzaHutStore = StoreMother.AEntityStore(15, "pizza hut");
		await context.Stores.AddAsync(pizzaHutStore);
		await context.SaveChangesAsync();

		Pizza pizza1 = PizzaMother.AEntityPizza(181, "banana pizza", pizzaHutStore);
		Pizza pizza2 = PizzaMother.AEntityPizza(182, "pineapple pizza", pizzaHutStore);
		await context.Pizzas.AddAsync(pizza1);
		await context.Pizzas.AddAsync(pizza2);
		await context.SaveChangesAsync();

		User user1 = UserMother.AEntityUser(16, "jack");
		User user2 = UserMother.AEntityUser(18, "tom");
		await context.Users.AddAsync(user1);
		await context.Users.AddAsync(user2);
		await context.SaveChangesAsync();

		UserPizzaPreference preference1 = UserPizzaPreferenceMother.AEntityUserPizzaPreference(6, pizza1, user1);
		UserPizzaPreference preference2 = UserPizzaPreferenceMother.AEntityUserPizzaPreference(91, pizza2, user2);
		UserPizzaPreference preference3 = UserPizzaPreferenceMother.AEntityUserPizzaPreference(50, pizza2, user1);
		await context.UserPizzaPreferences.AddAsync(preference1);
		await context.UserPizzaPreferences.AddAsync(preference2);
		await context.UserPizzaPreferences.AddAsync(preference3);
		await context.SaveChangesAsync();
	}
}
