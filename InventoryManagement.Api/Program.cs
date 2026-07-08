using InventoryManagement.Business.Interfaces;
using InventoryManagement.Business.Mapping;
using InventoryManagement.Business.Services;
using InventoryManagement.Data.Context;
using InventoryManagement.Data.Interfaces;
using InventoryManagement.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfile));

builder.Services.AddDbContext<DbInventoryContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
