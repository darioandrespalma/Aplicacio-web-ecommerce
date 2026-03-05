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
// ----------------------------------------------------

// --- REGISTRO DE REPOSITORIOS ---
builder.Services.AddScoped<BodegaHogar.Application.Interfaces.IProductRepository, BodegaHogar.Infrastructure.Repositories.ProductRepository>();
// --------------------------------

builder.Services.AddScoped<BodegaHogar.Application.Interfaces.ICategoryRepository, BodegaHogar.Infrastructure.Repositories.CategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();