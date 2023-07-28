using Microsoft.Playwright;
using static Pizzaaa.Playwright.PizzaWorld.PizzaContainerUtils;

namespace Pizzaaa.Playwright.PizzaWorld;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class TabsTest : BaseScreenshotTest
{
	public override string GetTraceName()
	{
		return "Tabs";
	}

	[Test]
	public async Task ChangingTabs_ChangesTableResult()
	{
		await GetTabByStoreName(Page, "Canton").ClickAsync();

		await Expect(GetFirstRowPizzaNameColumn()).ToHaveTextAsync("Schiacciata");

		await GetTabByStoreName(Page, "Ciacco").ClickAsync();

		await Expect(GetFirstRowPizzaNameColumn()).ToHaveTextAsync("Margherita");

		await GetTabByStoreName(Page, "Canton").First.ClickAsync();

		await Expect(GetFirstRowPizzaNameColumn()).ToHaveTextAsync("Schiacciata");
	}

	private ILocator GetFirstRowPizzaNameColumn()
	{
		return Page.GetByRole(AriaRole.Row)
			.Nth(1)
			.GetByRole(AriaRole.Cell)
			.Nth(PIZZA_NAME_COLUMN_INDEX);
	}
}
