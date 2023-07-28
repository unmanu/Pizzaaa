using static Pizzaaa.Playwright.PizzaWorld.PizzaContainerUtils;

namespace Pizzaaa.Playwright.PizzaWorld;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class SelectionTest : BaseScreenshotTest
{
	public override string GetTraceName()
	{
		return "Selection";
	}

	[Test]
	public async Task SelectingRow_UpdatesTabCounter()
	{
		await DoSelectPizza(Page, "Schiacciata");

		await Expect(GetTabByStoreName(Page, "Canton")).ToContainTextAsync("1");

		await DoSelectPizza(Page, "Cotto");

		await Expect(GetTabByStoreName(Page, "Canton")).ToContainTextAsync("2");

		await DoUnselectPizza(Page, "Cotto");

		await Expect(GetTabByStoreName(Page, "Canton")).ToContainTextAsync("1");
	}
}
