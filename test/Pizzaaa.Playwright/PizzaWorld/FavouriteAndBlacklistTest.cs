using Microsoft.Playwright;
using Pizzaaa.Playwright.TestUtils;
using static Pizzaaa.Playwright.PizzaWorld.PizzaContainerUtils;

namespace Pizzaaa.Playwright.PizzaWorld;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class FavouriteAndBlacklistTest : BaseScreenshotTest
{
	public override string GetTraceName()
	{
		return "FavouriteAndBlacklist";
	}

	[Test]
	public async Task CheckingFavouriteAndBlacklist_OnlyOneCanBeCheckedInAGivenTime()
	{
		await PlaywrightUtils.DoLogin(Page);

		var blacklistCheckbox = GetRowByPizzaName(Page, "Schiacciata").GetByRole(AriaRole.Cell).Nth(BLACKLIST_COLUMN_INDEX).GetByRole(AriaRole.Checkbox);
		var favouriteCheckbox = GetRowByPizzaName(Page, "Schiacciata").GetByRole(AriaRole.Cell).Nth(FAVOURITE_COLUMN_INDEX).GetByRole(AriaRole.Checkbox);

		await blacklistCheckbox.UncheckAsync();
		await favouriteCheckbox.UncheckAsync();
		await Expect(blacklistCheckbox).ToBeCheckedAsync(new() { Checked = false });
		await Expect(favouriteCheckbox).ToBeCheckedAsync(new() { Checked = false });

		await blacklistCheckbox.CheckAsync();
		await Expect(blacklistCheckbox).ToBeCheckedAsync();
		await Expect(favouriteCheckbox).ToBeCheckedAsync(new() { Checked = false });

		await favouriteCheckbox.CheckAsync();
		await Expect(favouriteCheckbox).ToBeCheckedAsync();
		await Expect(blacklistCheckbox).ToBeCheckedAsync(new() { Checked = false });

		await blacklistCheckbox.CheckAsync();
		await Expect(blacklistCheckbox).ToBeCheckedAsync();
		await Expect(favouriteCheckbox).ToBeCheckedAsync(new() { Checked = false });

		await blacklistCheckbox.UncheckAsync();
		await Expect(blacklistCheckbox).ToBeCheckedAsync(new() { Checked = false });
		await Expect(favouriteCheckbox).ToBeCheckedAsync(new() { Checked = false });

		await favouriteCheckbox.CheckAsync();
		await Expect(favouriteCheckbox).ToBeCheckedAsync();
		await Expect(blacklistCheckbox).ToBeCheckedAsync(new() { Checked = false });

		await favouriteCheckbox.UncheckAsync();
		await Expect(blacklistCheckbox).ToBeCheckedAsync(new() { Checked = false });
		await Expect(favouriteCheckbox).ToBeCheckedAsync(new() { Checked = false });
	}
}