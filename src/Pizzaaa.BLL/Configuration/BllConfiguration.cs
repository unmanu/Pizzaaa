using Microsoft.Extensions.DependencyInjection;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.Services;
using Pizzaaa.BLL.Services.Interfaces;
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

		services.AddScoped<IPizzaService, PizzaService>();
		services.AddScoped<IStoreService, StoreService>();
		services.AddScoped<IIngredientService, IngredientService>();
		services.AddScoped<IUserPizzaPreferenceService, UserPizzaPreferenceService>();
		services.AddScoped<IOrderService, OrderService>();

		return services;
	}
}