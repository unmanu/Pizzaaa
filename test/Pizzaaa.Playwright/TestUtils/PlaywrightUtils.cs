using Microsoft.Playwright;

namespace Pizzaaa.Playwright.TestUtils;

public static class PlaywrightUtils
{
	public static string GetBaseUrl(string route = "/pizza")
	{
		if (!string.IsNullOrWhiteSpace(route) && !route.StartsWith("/"))
		{
			route = "/" + route;
		}
		return "http://localhost:5254" + (route ?? "");
	}

	public static async Task StopTracing(IBrowserContext context, string testName)
	{
		await context.Tracing.StopAsync(new()
		{
			Path = $"trace{testName}.zip"
		});
	}

	public static async Task DoLogin(IPage page)
	{
		await page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
		await page.GetByRole(AriaRole.Textbox).First.ClickAsync();
		await page.GetByRole(AriaRole.Textbox).First.FillAsync("test");
		await page.GetByRole(AriaRole.Textbox).First.PressAsync("Tab");
		await page.Locator("input[type=\"password\"]").FillAsync("test");
		await page.Locator("input[type=\"password\"]").PressAsync("Tab");
		await page.GetByRole(AriaRole.Link, new() { Name = "Login/Registrati" }).ClickAsync();
	}
}
