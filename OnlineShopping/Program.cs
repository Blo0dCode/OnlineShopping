using Microsoft.EntityFrameworkCore;
using OnlineShopping.DAL;
using OnlineShopping.DAL.Interfaces;
using OnlineShopping.DAL.Repositories;
using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.Implementations;
using OnlineShopping.Domain.Interface;
using OnlineShopping.Service.Implementations;
using OnlineShopping.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseNpgsql(connection));

builder.Services.AddScoped<IBaseRepository<Product>, ProductRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBaseRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IMapper, Mapper>(); //TODO правильно ли я его всунул

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();