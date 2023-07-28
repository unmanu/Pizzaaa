using Microsoft.Playwright;
using static Pizzaaa.Playwright.PizzaWorld.PizzaContainerUtils;

namespace Pizzaaa.Playwright.PizzaWorld;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class TableUserNotLoggedTest : BaseScreenshotTest
{
	public override string GetTraceName()
	{
		return "TableUserNotLogged";
	}

	[Test]
	public async Task NotLoggedUser_ShowsTableWithoutPreferences()
	{
		var tableColumns = Page.GetByRole(AriaRole.Columnheader);

		await Expect(tableColumns).ToHaveCountAsync(5);

		var columns = await tableColumns.AllAsync();
		await Expect(columns[SELECTION_CHECKBOX_COLUMN_INDEX]).ToBeEmptyAsync();
		await Expect(columns[PIZZA_NAME_COLUMN_INDEX]).ToContainTextAsync("Nome");
		await Expect(columns[INGREDIENTS_COLUMN_INDEX]).ToContainTextAsync("Ingredienti");
		await Expect(columns[PRICE_COLUMN_INDEX]).ToContainTextAsync("Prezzo");
		await Expect(columns[NOT_LOGGED_ACTIONS_COLUMN_INDEX]).ToBeEmptyAsync();
	}

}