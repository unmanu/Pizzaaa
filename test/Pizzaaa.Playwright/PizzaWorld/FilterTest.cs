using Microsoft.Playwright;
using Pizzaaa.Playwright.TestUtils;
using static Pizzaaa.Playwright.PizzaWorld.PizzaContainerUtils;

namespace Pizzaaa.Playwright.PizzaWorld;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class FilterTest : BaseScreenshotTest
{
	public override string GetTraceName()
	{
		return "Filter";
	}

	[Test]
	public async Task ApplyingFilter_ReduceVisibleRows()
	{
		await PlaywrightUtils.DoLogin(Page);

		await Page.Locator(".includeIngredientCombo")
			.ClickAsync();

		await Page.GetByTestId("checkcombo-include-ingredient")
			.Filter(new() { HasText = "pomodoro" })
			.ClickAsync();

		await Page.Locator(".mud-overlay").ClickAsync();


		await Page.Locator(".excludeIngredientCombo")
			.ClickAsync();

		await Page.GetByTestId("checkcombo-exclude-ingredient")
			.Filter(new() { HasText = "olio" })
			.First
			.ClickAsync();

		await Page.Locator(".mud-overlay").ClickAsync();

		await GetTabByStoreName(Page, "Canton").ClickAsync();

		await DoFavouritePizza(Page, "Margherita");
		await DoBlacklistPizza(Page, "Cotto");
		await DoFavouritePizza(Page, "Wurstel");

		await GetTabByStoreName(Page, "Ciacco").ClickAsync();

		await DoFavouritePizza(Page, "Margherita");
		await DoFavouritePizza(Page, "Marinara");
		await DoBlacklistPizza(Page, "Romana");

		await GetTabByStoreName(Page, "Canton").ClickAsync();


		await Page.GetByLabel("Escludi blacklist").CheckAsync();

		await Page.GetByLabel("Solo favoriti").CheckAsync();

		await Page.GetByPlaceholder("Search").ClickAsync();

		await Page.GetByPlaceholder("Search").FillAsync("Mar");

		await Page.Locator(".mud-tabs-panels").ClickAsync();


		await Expect(Page.GetByRole(AriaRole.Row)).ToHaveCountAsync(2);
		await Expect(Page.GetByRole(AriaRole.Row)
			.Nth(1)
			.GetByRole(AriaRole.Cell)
			.Nth(PIZZA_NAME_COLUMN_INDEX)).ToContainTextAsync("Margherita");

		await GetTabByStoreName(Page, "Ciacco").ClickAsync();


		await Expect(Page.GetByRole(AriaRole.Row)).ToHaveCountAsync(3);
		await Expect(Page.GetByRole(AriaRole.Row)
			.Nth(1)
			.GetByRole(AriaRole.Cell)
			.Nth(PIZZA_NAME_COLUMN_INDEX)).ToContainTextAsync("Margherita");
		await Expect(Page.GetByRole(AriaRole.Row)
			.Nth(2)
			.GetByRole(AriaRole.Cell)
			.Nth(PIZZA_NAME_COLUMN_INDEX)).ToContainTextAsync("Marinara");

	}
}