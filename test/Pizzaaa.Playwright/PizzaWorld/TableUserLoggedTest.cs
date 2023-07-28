using Microsoft.Playwright;
using Pizzaaa.Playwright.TestUtils;
using static Pizzaaa.Playwright.PizzaWorld.PizzaContainerUtils;

namespace Pizzaaa.Playwright.PizzaWorld;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class TableUserLoggedTest : BaseScreenshotTest
{
	public override string GetTraceName()
	{
		return "TableUserLogged";
	}

	[Test]
	public async Task LoggedUser_ShowsTableWithPreferences()
	{
		await PlaywrightUtils.DoLogin(Page);

		var tableColumns = Page.GetByRole(AriaRole.Columnheader);

		await Expect(tableColumns).ToHaveCountAsync(7);

		var columns = await tableColumns.AllAsync();
		await Expect(columns[SELECTION_CHECKBOX_COLUMN_INDEX]).ToBeEmptyAsync();
		await Expect(columns[PIZZA_NAME_COLUMN_INDEX]).ToContainTextAsync("Nome");
		await Expect(columns[INGREDIENTS_COLUMN_INDEX]).ToContainTextAsync("Ingredienti");
		await Expect(columns[PRICE_COLUMN_INDEX]).ToContainTextAsync("Prezzo");
		await Expect(columns[BLACKLIST_COLUMN_INDEX]).ToContainTextAsync("Blacklist");
		await Expect(columns[FAVOURITE_COLUMN_INDEX]).ToContainTextAsync("Favourite");
		await Expect(columns[LOGGED_ACTIONS_COLUMN_INDEX]).ToBeEmptyAsync();
	}
}
