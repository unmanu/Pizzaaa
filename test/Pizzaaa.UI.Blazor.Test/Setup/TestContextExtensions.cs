﻿using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using MudExtensions.Services;

namespace Pizzaaa.UI.Blazor.Test.Setup;


public static class TestContextExtensions
{
	public static void AddTestServices(this TestContext ctx)
	{
		ctx.JSInterop.Mode = JSRuntimeMode.Loose;
		ctx.Services.AddSingleton<NavigationManager>(new MockNavigationManager());
		ctx.Services.AddMudServices(options =>
		{
			options.SnackbarConfiguration.ShowTransitionDuration = 0;
			options.SnackbarConfiguration.HideTransitionDuration = 0;
		});
		ctx.Services.AddMudExtensions();
		ctx.Services.AddScoped(sp => new HttpClient());
		ctx.Services.AddOptions();
	}
}
