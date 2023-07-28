using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.Services.Interfaces;
using Pizzaaa.BLL.System.Interfaces;
using Pizzaaa.UI.Blazor.Pages.PizzaWorld;
using Pizzaaa.UI.Blazor.Test.Setup;

namespace Pizzaaa.UI.Blazor.Test.Pages.PizzaWorld;

public class PizzaContainerTest : BunitTest
{
	private readonly Mock<IStoreService> _mockStoreService;
	private readonly Mock<IPizzaService> _mockPizzaService;
	private readonly Mock<IUserPizzaPreferenceService> _mockUserPizzaPreferenceService;
	private readonly Mock<ISecurityService> _mockSecurityService;
	private readonly Mock<IOrderService> _mockOrderService;
	private readonly Mock<IIngredientService> _mockIngredientService;
	private readonly Mock<IRandomService> _mockRandomService;

	public PizzaContainerTest()
	{
		_mockStoreService = new();
		_mockPizzaService = new();
		_mockUserPizzaPreferenceService = new();
		_mockSecurityService = new();
		_mockOrderService = new();
		_mockIngredientService = new();
		_mockRandomService = new();

		Context.Services.AddSingleton<IStoreService>(_mockStoreService.Object);
		Context.Services.AddSingleton<IPizzaService>(_mockPizzaService.Object);
		Context.Services.AddSingleton<IUserPizzaPreferenceService>(_mockUserPizzaPreferenceService.Object);
		Context.Services.AddSingleton<ISecurityService>(_mockSecurityService.Object);
		Context.Services.AddSingleton<IOrderService>(_mockOrderService.Object);
		Context.Services.AddSingleton<IIngredientService>(_mockIngredientService.Object);
		Context.Services.AddSingleton<IRandomService>(_mockRandomService.Object);

		_mockStoreService.Setup(x => x.FindAll()).ReturnsAsync(new List<Store>());
		_mockUserPizzaPreferenceService.Setup(x => x.FindAllByUser()).ReturnsAsync(new List<UserPizzaPreference>());
		_mockOrderService.Setup(x => x.FindTodayOrders()).ReturnsAsync(new List<Order>());
		_mockPizzaService.Setup(x => x.FindAllByStore(It.IsAny<int>())).ReturnsAsync(new List<Pizza>());
		_mockIngredientService.Setup(x => x.FindAll()).ReturnsAsync(new List<Ingredient>());
	}

	[Fact]
	public void BunitTest()
	{
		IRenderedComponent<PizzaContainer> component = Context.RenderComponent<PizzaContainer>();

		Assert.NotEmpty(component.Find("table").InnerHtml);

	}
}
