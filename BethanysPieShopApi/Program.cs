using Application.OrderPurchases;
using Application.Products;
using Application.Suppliers;
using BethanysPieShop.InventoryManagement;
using BethanysPieShop.InventoryManagement.IRepository;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
   builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddTransient(typeof(IProductRepository), typeof(ProductRepository));
builder.Services.AddTransient(typeof(IOrderPurchaseRepository), typeof(OrderPurchaseRepository));
builder.Services.AddTransient(typeof(ISupplierRepository), typeof(SupplierRepository));


builder.Services.AddTransient<IProductsService, ProductsService>();
builder.Services.AddTransient<IOrderPurchasesService, OrderPurchasesService>();
builder.Services.AddTransient<ISuppliersService, SuppliersService>();


builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddSwaggerGen();

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
