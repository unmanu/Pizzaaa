using Microsoft.Extensions.DependencyInjection;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.Services;

namespace Pizzaaa.BLL.Configuration;

public static class BllConfiguration
{

    public static IServiceCollection AddBllModule(this IServiceCollection services)
    {
        services.AddScoped<SecurityService>();

        services.AddScoped<PizzaService>();
        services.AddScoped<StoreService>();
        services.AddScoped<IngredientService>();
        services.AddScoped<UserPizzaPreferenceService>();
        services.AddScoped<OrderService>();

        return services;
    }
}