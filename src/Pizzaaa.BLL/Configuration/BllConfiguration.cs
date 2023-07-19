using Microsoft.Extensions.DependencyInjection;
using Pizzaaa.BLL.Ports;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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