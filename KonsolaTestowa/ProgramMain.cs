/*using BLL;
using DAL;
using IBLL;
using IDAL_;
using Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KonsolaTestowa
{
    public class ProgramMain
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
        }
    }
}
*/