using Microsoft.Playwright.NUnit;
using Pizzaaa.Playwright.TestUtils;

namespace Pizzaaa.Playwright.PizzaWorld;

public abstract class BaseScreenshotTest : PageTest
{
	[SetUp]
	public async Task SetUp()
	{
		await Context.Tracing.StartAsync(new()
		{
			Screenshots = true,
			Snapshots = true,
			Sources = true
		});

		await Page.GotoAsync(PlaywrightUtils.GetBaseUrl());
	}

	[TearDown]
	public async Task TearDown()
	{
		await PlaywrightUtils.StopTracing(Context, GetTraceName());
	}

	public abstract string GetTraceName();
}