using MudBlazor.Services;
using MudExtensions.Services;
using Pizzaaa.BLL.Configuration;
using Pizzaaa.Persistance.Configuration;
using Pizzaaa.UI.Blazor.Data.Theme;
using Serilog.Formatting.Json;
using Serilog;

Log.Logger = new LoggerConfiguration()
	.WriteTo.File(new JsonFormatter(), Path.Combine("startup-logs", "scorecard-startup-log.json"), rollingInterval: RollingInterval.Day)
	.WriteTo.Console()
	.CreateBootstrapLogger();
try
{
	var builder = WebApplication.CreateBuilder(args);


	builder.Host.UseSerilog();

	Log.CloseAndFlush();
	Log.Logger = new LoggerConfiguration()
		.ReadFrom.Configuration(builder.Configuration)
		.CreateBootstrapLogger();

	// Add services to the container.
	builder.Services.AddRazorPages();
	builder.Services.AddServerSideBlazor();
	builder.Services.AddSingleton<ThemeService>();
	builder.Services.AddMudServices();
	builder.Services.AddMudExtensions();
	builder.Services.AddAuthentication("Cookies").AddCookie();

	PersistanceSettingsOptions persistanceSettingsOptions = new();
	builder.Configuration.GetSection(PersistanceSettingsOptions.PersistanceSettings).Bind(persistanceSettingsOptions);
	builder.Services.Configure<PersistanceSettingsOptions>(builder.Configuration.GetSection(PersistanceSettingsOptions.PersistanceSettings));
	builder.Services.AddPersistanceModule(persistanceSettingsOptions);
	builder.Services.AddBllModule();

	builder.Services.AddAutoMapper(typeof(PersistanceMapperProfile));
	builder.Services.AddHttpContextAccessor();



	var app = builder.Build();
	app.CreaDatabase(persistanceSettingsOptions);

	Log.Information("environment              : " + app.Environment.EnvironmentName);
	Log.Information("CreateDatabaseIfNotExists: " + persistanceSettingsOptions.CreateDatabaseIfNotExists);
	Log.Information("RecreateDatabase         : " + persistanceSettingsOptions.RecreateDatabase);
	Log.Information("SqliteInAppFolder        : " + persistanceSettingsOptions.SqliteInAppFolder);
	Log.Information("SqliteCustomFolder       : " + persistanceSettingsOptions.SqliteCustomFolder);
	Log.Information("JsonSourceInitializer    : " + persistanceSettingsOptions.JsonSourceInitializer);
	
	if (!app.Environment.IsDevelopment())
	{
		// Configure the HTTP request pipeline.
		app.UseExceptionHandler("/Error");
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
	}

	app.UseAuthentication();
	app.UseAuthorization();

	app.UseHttpsRedirection();

	app.UseStaticFiles();

	app.UseRouting();

	app.MapBlazorHub();
	app.MapFallbackToPage("/_Host");

	app.Run();

}
catch (Exception ex)
{
	// Use ForContext to give a context to this static environment (for Serilog LoggerNameEnricher).
	Log.ForContext<Program>().Fatal(ex, $"Host terminated unexpectedly");
}
finally
{
	Log.CloseAndFlush();
}
