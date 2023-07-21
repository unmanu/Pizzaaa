using Microsoft.Extensions.DependencyInjection;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.Services;
using Pizzaaa.BLL.System;
using Pizzaaa.BLL.System.Interfaces;

namespace Pizzaaa.BLL.Configuration;

public static class BllConfiguration
{

	public static IServiceCollection AddBllModule(this IServiceCollection services)
	{
		services.AddScoped<ISecurityService, SecurityService>();
		services.AddSingleton<IRandomService, RandomService>();
		services.AddSingleton<IDateService, DateService>();

		services.AddScoped<PizzaService>();
		services.AddScoped<StoreService>();
		services.AddScoped<IngredientService>();
		services.AddScoped<UserPizzaPreferenceService>();
		services.AddScoped<OrderService>();

		return services;
	}
}