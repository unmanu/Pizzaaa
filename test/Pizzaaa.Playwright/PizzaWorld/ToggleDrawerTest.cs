using Microsoft.Playwright;
using System.Text.RegularExpressions;
using static Pizzaaa.Playwright.PizzaWorld.PizzaContainerUtils;

namespace Pizzaaa.Playwright.PizzaWorld;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ToggleDrawerTest : BaseScreenshotTest
{
	public override string GetTraceName()
	{
		return "ToggleDrawer";
	}

	[Test]
	public async Task Toggling_OpenAndClosesDrawer()
	{
		await Expect(Page.GetByTestId("orderDrawer"))
			.ToHaveClassAsync(new Regex("mud-drawer--closed"));

		await Page.GetByRole(AriaRole.Button, new() { Name = "Toggle end" }).ClickAsync();

		await Expect(Page.GetByTestId("orderDrawer"))
			.ToHaveClassAsync(new Regex("mud-drawer--open"));

		await Page.GetByRole(AriaRole.Button, new() { Name = "Toggle end" }).ClickAsync();

		await Expect(Page.GetByTestId("orderDrawer"))
			.ToHaveClassAsync(new Regex("mud-drawer--closed"));
	}

}