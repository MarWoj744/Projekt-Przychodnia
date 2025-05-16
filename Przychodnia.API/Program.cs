using BLL;
using DAL;
using IBLL;
using IDAL_;
using Microsoft.EntityFrameworkCore;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddScoped<IBadanieRepository, BadanieRepository>();
builder.Services.AddScoped<ILekarzRepository, LekarzRepository>();
builder.Services.AddScoped<IOsobaRepository, OsobaRepository>();
builder.Services.AddScoped<IPacjentRepository, PacjentRepository>();
builder.Services.AddScoped<IRecepcjonistkaRepository, RecepcjonistkaRepository>();
builder.Services.AddScoped<IWizytaRepository, WizytaRepository>();
builder.Services.AddScoped<IWykonaneBadaniaRepository, WykonaneBadaniaRepository>();

builder.Services.AddScoped<IBadanieService, BadanieService>();
builder.Services.AddScoped<ILekarzService, LekarzService>();
builder.Services.AddScoped<IOsobaService, OsobaService>();
builder.Services.AddScoped<IPacjentService, PacjentService>();
builder.Services.AddScoped<IRecepcjonistkaService, RecepcjonistkaService>();
builder.Services.AddScoped<IWizytaService, WizytaService>();
builder.Services.AddScoped<IWykonaneBadanieService, WykonaneBadaniaService>();

builder.Services.AddDbContext<DbPrzychodnia>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
