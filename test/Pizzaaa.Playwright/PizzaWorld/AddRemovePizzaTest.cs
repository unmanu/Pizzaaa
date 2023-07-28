using Microsoft.Playwright;
using System.Text.RegularExpressions;
using static Pizzaaa.Playwright.PizzaWorld.PizzaContainerUtils;

namespace Pizzaaa.Playwright.PizzaWorld;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class AddRemoveTest : BaseScreenshotTest
{
	public override string GetTraceName()
	{
		return "AddRemove";
	}

	[Test]
	public async Task AddingPizza_OpensDrawerAndYouCanRemoveIt()
	{
		await Expect(Page.GetByTestId("orderDrawer"))
			.ToHaveClassAsync(new Regex("mud-drawer--closed"));

		await GetTabByStoreName(Page, "Canton").ClickAsync();

		await GetRowByPizzaName(Page, "Marinara")
			.GetByRole(AriaRole.Cell)
			.Nth(NOT_LOGGED_ACTIONS_COLUMN_INDEX)
			.GetByRole(AriaRole.Button)
			.ClickAsync();

		await Expect(Page.GetByTestId("orderDrawer"))
			.ToHaveClassAsync(new Regex("mud-drawer--open"));

		var firstStoreOrders = GetOrderByStoreName(Page, "Canton");

		await Expect(firstStoreOrders).ToContainTextAsync("Marinara TODO");

	}

}
