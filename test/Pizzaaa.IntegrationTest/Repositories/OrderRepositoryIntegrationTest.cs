using Moq;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.System.Interfaces;
using Pizzaaa.IntegrationTest.Mothers;
using Pizzaaa.IntegrationTest.Setup;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories;

namespace Pizzaaa.IntegrationTest.Repositories;

public class OrderRepositoryIntegrationTest : BaseDatabaseTest
{
	private readonly Mock<ISecurityService> _mockSecurityService;
	private readonly Mock<IDateService> _mockDateService;
	private readonly DateOnly today;

	public OrderRepositoryIntegrationTest()
	{
		_mockSecurityService = new Mock<ISecurityService>();
		_mockDateService = new Mock<IDateService>();
		today = new DateOnly(2023, 3, 27);
	}

	[Fact]
	public async Task FindTodayOrders_ThereAreOrdersForToday_ReturnsOrderWithPizzaData()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		OrderRepository repository = new(context, _mockSecurityService.Object, _mockDateService.Object);

		_mockDateService.Setup(mock => mock.GetTodayDateOnly()).Returns(today);

		List<Order> result = await repository.FindTodayOrders();

		Assert.NotNull(result);
		Assert.Single(result);
		Assert.Equal("bobson", result.First().OrderUser);
		Assert.NotNull(result.First().Pizza);
		Assert.Equal(27, result.First().Pizza.ID);
		Assert.Equal("pepperoni", result.First().Pizza.Name);
	}

	[Fact]
	public async Task FindTodayOrders_NoOrdersForToday_ReturnsEmptyList()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		OrderRepository repository = new(context, _mockSecurityService.Object, _mockDateService.Object);

		_mockDateService.Setup(mock => mock.GetTodayDateOnly()).Returns(new DateOnly(9999, 12, 31));

		List<Order> result = await repository.FindTodayOrders();

		Assert.NotNull(result);
		Assert.Empty(result);
	}

	private async Task DbTestInitializer(PizzaContext context)
	{
		Store store = StoreMother.AEntityStore(15, "pizza hut");
		await context.Stores.AddAsync(store);
		await context.SaveChangesAsync();
		Pizza pizza = PizzaMother.AEntityPizza(27, "pepperoni", store);
		await context.Pizzas.AddAsync(pizza);
		await context.SaveChangesAsync();

		Order order1 = OrderMother.AEntityOrder(6, new DateOnly(2023, 1, 13), "jackson", pizza, store);
		Order order2 = OrderMother.AEntityOrder(91, new DateOnly(2023, 2, 25), "timson", pizza, store);
		Order order3 = OrderMother.AEntityOrder(50, today, "bobson", pizza, store);
		await context.Orders.AddAsync(order1);
		await context.Orders.AddAsync(order2);
		await context.Orders.AddAsync(order3);
		await context.SaveChangesAsync();
	}
}
