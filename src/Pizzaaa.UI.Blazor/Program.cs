using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using Pizzaaa.BLL.Services;
using Pizzaaa.Persistance.Configuration;
using Pizzaaa.BLL.Configuration;
using Pizzaaa.UI.Blazor.Data;
using Pizzaaa.UI.Blazor.Data.Theme;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<ThemeService>();
builder.Services.AddMudServices();
builder.Services.AddAuthentication("Cookies").AddCookie();

PersistanceSettingsOptions persistanceSettingsOptions = new();
builder.Configuration.GetSection(PersistanceSettingsOptions.PersistanceSettings).Bind(persistanceSettingsOptions);
builder.Services.Configure<PersistanceSettingsOptions>(builder.Configuration.GetSection(PersistanceSettingsOptions.PersistanceSettings));
builder.Services.AddPersistanceModule(persistanceSettingsOptions);
builder.Services.AddBllModule();

builder.Services.AddAutoMapper(typeof(PersistanceMapperProfile));
builder.Services.AddHttpContextAccessor();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.CreaDatabase(persistanceSettingsOptions);
}
else
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
