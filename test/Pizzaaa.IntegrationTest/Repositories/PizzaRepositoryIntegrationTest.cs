using Moq;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.System.Interfaces;
using Pizzaaa.IntegrationTest.Mothers;
using Pizzaaa.IntegrationTest.Setup;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories;

namespace Pizzaaa.IntegrationTest.Repositories;

public class PizzaRepositoryIntegrationTest : BaseDatabaseTest
{
	private readonly Mock<ISecurityService> _mockSecurityService;
	private readonly Mock<IDateService> _mockDateService;

	public PizzaRepositoryIntegrationTest()
	{
		_mockSecurityService = new Mock<ISecurityService>();
		_mockDateService = new Mock<IDateService>();
	}

	[Fact]
	public async Task FindAllByStore_FindsPizzaForStore_ReturnsOnlyPizzaFromThatStore()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		PizzaRepository repository = new(context, _mockSecurityService.Object, _mockDateService.Object);

		List<Pizza> result = await repository.FindAllByStore(15);

		Assert.NotNull(result);
		Assert.Equal(2, result.Count);
		Assert.DoesNotContain("pepperoni pizza", result.Select(x => x.Name));
		Assert.Contains("banana pizza", result.Select(x => x.Name));
		Assert.Contains("pineapple pizza", result.Select(x => x.Name));
	}

	[Fact]
	public async Task FindAllByStore_FindsNoPizzaForStore_ReturnsEmptyList()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		PizzaRepository repository = new(context, _mockSecurityService.Object, _mockDateService.Object);

		List<Pizza> result = await repository.FindAllByStore(9999);

		Assert.NotNull(result);
		Assert.Empty(result);
	}


	private async Task DbTestInitializer(PizzaContext context)
	{
		Store pizzaHutStore = StoreMother.AEntityStore(15, "pizza hut");
		Store dominoStore = StoreMother.AEntityStore(27, "domino");
		await context.Stores.AddAsync(pizzaHutStore);
		await context.Stores.AddAsync(dominoStore);
		await context.SaveChangesAsync();

		Pizza pizza1 = PizzaMother.AEntityPizza(6, "pepperoni pizza", dominoStore);
		Pizza pizza2 = PizzaMother.AEntityPizza(91, "banana pizza", pizzaHutStore);
		Pizza pizza3 = PizzaMother.AEntityPizza(50, "pineapple pizza", pizzaHutStore);
		await context.Pizzas.AddAsync(pizza1);
		await context.Pizzas.AddAsync(pizza2);
		await context.Pizzas.AddAsync(pizza3);
		await context.SaveChangesAsync();
	}
}
