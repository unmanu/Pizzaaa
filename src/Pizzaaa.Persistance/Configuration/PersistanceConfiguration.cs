using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pizzaaa.BLL.Ports;
using Pizzaaa.Persistance.Adapters;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Configuration;

public static class PersistanceConfiguration
{
	public static IHost CreaDatabase(this IHost host, PersistanceSettingsOptions settings)
	{
		if (!settings.RecreateDatabase && !settings.CreateDatabaseIfNotExists && settings.GetEnumTipoDatabase() != PersistanceType.InMemory)
		{
			return host;
		}

		using (var scope = host.Services.CreateScope())
		{
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<PizzaContext>();
			if (settings.RecreateDatabase)
			{
				context.Database.EnsureDeleted();
			}
			context.Database.EnsureCreated();
			DbInitializer.Initialize(context);
		}
		return host;
	}

	public static IServiceCollection AddPersistanceModule(this IServiceCollection services, PersistanceSettingsOptions settings)
	{
		services.AddDatabaseConnection(settings);

		services.AddScoped<IPizzaPort, DbPizzaAdapter>();
		services.AddScoped<IUserPort, DbUserAdapter>();

		services.AddScoped<IngredientRepository>();
		services.AddScoped<PizzaRepository>();
		services.AddScoped<StoreRepository>();
		services.AddScoped<UserRepository>();

		return services;
	}

	public static IServiceCollection AddDatabaseConnection(this IServiceCollection services, PersistanceSettingsOptions settings)
	{
		switch (settings.GetEnumTipoDatabase())
		{
			case PersistanceType.SqlServer:
				services.AddSqlServer<PizzaContext>(GetStringaConnessioneSqlServer(settings));
				break;
			case PersistanceType.Sqlite:
				services.AddSqlite<PizzaContext>(GetStringaConnessioneSqlite(settings));
				break;
			case PersistanceType.InMemory:
				services.AddDbContext<PizzaContext>(options => options.UseInMemoryDatabase(settings.ConnectionString ?? "PizzaInMemory"));
				break;
		}
		return services;
	}

	private static string GetStringaConnessioneSqlServer(PersistanceSettingsOptions settings)
	{
		return settings.ConnectionString ?? $"Server={settings.Host};Port={settings.Port};Database={settings.Database};User={settings.User};Password={settings.Password};Trusted_Connection=False;Pooling=true;";
	}

	private static string GetStringaConnessioneSqlite(PersistanceSettingsOptions settings)
	{
		if (!string.IsNullOrEmpty(settings.ConnectionString))
		{
			return settings.ConnectionString;
		}

		if (settings.SqliteInAppFolder)
		{
			return $"Data Source=pizza.db";
		}
		else
		{
			string cartellaLocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			string databaseFolder = Path.Join(cartellaLocalAppData, "Nightwood", "Pizza");
			if (!Directory.Exists(databaseFolder))
			{
				Directory.CreateDirectory(databaseFolder);
			}
			string databasePath = Path.Join(databaseFolder, "pizza.db");
			Console.WriteLine($"sqlite .db path {databasePath}");
			return $"Data Source={databasePath}";
		}
	}

}