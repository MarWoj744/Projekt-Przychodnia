using BLL;
using DAL;
using IBLL;
using IDAL_;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerOptions =>
{
    swaggerOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Tutaj wklej swój JWT token."
    });

    swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


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
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddDbContext<DbPrzychodnia>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddCors(corsBuilder =>
    corsBuilder.AddPolicy("PolitykaCORS", policyBuilder =>
        policyBuilder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().Build()));

var jwtSettings = builder.Configuration.GetSection("JWT");
var signingKey = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
{
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(signingKey)
    };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DbPrzychodnia>();

    db.Database.Migrate();
    DbInit.Seed(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PolitykaCORS");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


//dotnet ef migrations add NewMigrationName --project Models --startup-project Przychodnia.API
//dotnet ef database update --project Models --startup-project Przychodnia.API