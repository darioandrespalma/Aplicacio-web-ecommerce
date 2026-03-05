using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BodegaHogar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- INYECCIÓN DE DEPENDENCIA DE LA BASE DE DATOS ---
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- REGISTRO DE REPOSITORIOS ---
builder.Services.AddScoped<BodegaHogar.Application.Interfaces.IProductRepository, BodegaHogar.Infrastructure.Repositories.ProductRepository>();
builder.Services.AddScoped<BodegaHogar.Application.Interfaces.ICategoryRepository, BodegaHogar.Infrastructure.Repositories.CategoryRepository>();


// --- CONFIGURACIÓN DE JWT (OBLIGATORIO ANTES DEL BUILD) ---
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// --- MIDDLEWARES DE SEGURIDAD (EL ORDEN ES VITAL) ---
app.UseAuthentication(); // 1. Primero verifica el Token (Identidad)
app.UseAuthorization();  // 2. Luego verifica los Permisos (Roles)
// ----------------------------------------------------

app.MapControllers();

app.Run();