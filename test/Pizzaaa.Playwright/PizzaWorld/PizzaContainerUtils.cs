using Microsoft.Playwright;

namespace Pizzaaa.Playwright.PizzaWorld;

public static class PizzaContainerUtils
{
	public const int SELECTION_CHECKBOX_COLUMN_INDEX = 0;
	public const int PIZZA_NAME_COLUMN_INDEX = 1;
	public const int INGREDIENTS_COLUMN_INDEX = 2;
	public const int PRICE_COLUMN_INDEX = 3;
	public const int NOT_LOGGED_ACTIONS_COLUMN_INDEX = 4;
	public const int LOGGED_ACTIONS_COLUMN_INDEX = 6;
	public const int BLACKLIST_COLUMN_INDEX = 4;
	public const int FAVOURITE_COLUMN_INDEX = 5;


	public static ILocator GetRowByPizzaName(IPage page, string pizzaName)
	{
		return page.GetByTestId("pizzaTable")
			.GetByRole(AriaRole.Row)
			.Filter(new()
			{
				Has = page.GetByRole(AriaRole.Cell)
				  .Nth(PIZZA_NAME_COLUMN_INDEX)
				  .GetByText(pizzaName)
			});
	}
	public static ILocator GetTabByStoreName(IPage page, string storeName)
	{
		return page.GetByTestId("storeTabs")
			.GetByText(storeName)
			.First;
	}

	public static ILocator GetOrderByStoreName(IPage page, string storeName)
	{
		return page.GetByTestId("orderStoreCard")
			.Filter(new()
			{
				Has = page.Locator("p")
					.GetByText(storeName)
			});
	}

	public static async Task DoSelectPizza(IPage page, string pizzaName)
	{
		await UpdateTableCheckBox(page, pizzaName, SELECTION_CHECKBOX_COLUMN_INDEX, true);
	}

	public static async Task DoUnselectPizza(IPage page, string pizzaName)
	{
		await UpdateTableCheckBox(page, pizzaName, SELECTION_CHECKBOX_COLUMN_INDEX, false);
	}

	public static async Task DoFavouritePizza(IPage page, string pizzaName)
	{
		await UpdateTableCheckBox(page, pizzaName, FAVOURITE_COLUMN_INDEX, true);
	}

	public static async Task DoUnfavouritePizza(IPage page, string pizzaName)
	{
		await UpdateTableCheckBox(page, pizzaName, FAVOURITE_COLUMN_INDEX, false);
	}

	public static async Task DoBlacklistPizza(IPage page, string pizzaName)
	{
		await UpdateTableCheckBox(page, pizzaName, BLACKLIST_COLUMN_INDEX, true);
	}

	public static async Task DoUnblacklistPizza(IPage page, string pizzaName)
	{
		await UpdateTableCheckBox(page, pizzaName, BLACKLIST_COLUMN_INDEX, false);
	}

	private static async Task UpdateTableCheckBox(IPage page, string pizzaName, int index, bool check)
	{
		var checkBox = GetRowByPizzaName(page, pizzaName)
			.GetByRole(AriaRole.Cell)
			.Nth(index)
			.GetByRole(AriaRole.Checkbox);

		if (check)
		{
			await checkBox.CheckAsync();
		}
		else
		{
			await checkBox.UncheckAsync();
		}
	}
}
