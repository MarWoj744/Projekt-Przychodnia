using Przychodnia.Services;
using Przychodnia.Models;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbPrzychodnia>();
builder.Services.AddScoped<IWizytaService, WizytaService>();
builder.Services.AddScoped<IWykonaneBadanieService, WykonaneBadaniaService>();
builder.Services.AddScoped<IOsobaService, OsobaService>();
builder.Services.AddScoped<IWizytaRepository, WizytaRepository>();
builder.Services.AddScoped<IWykonaneBadaniaRepository, WykonaneBadaniaRepository>();
builder.Services.AddScoped<IOsobaRepository, OsobaRepository>();
builder.Services.AddScoped<IPacjentService, PacjentService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
