using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Northwind.Data.Logic.Data.Northwind.Context;
using Common.UnitOfWork;
using EntityFrameworkCore.UseRowNumberForPaging;
using Northwind.MVC.Logic.Interfaces;
using Northwind.MVC.Logic.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<NorthWindContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("NorthwindConnection"), builder => builder.UseRowNumberForPaging());
}, ServiceLifetime.Scoped);

builder.Services.AddUnitOfWork<NorthWindContext>();
builder.Services.AddScoped<IProductsServices, ProdutcsServices>();
builder.Services.AddScoped<ICategoriesServices, CategoriesServices>();
builder.Services.AddScoped<ISuppliersServices, SuppliersServices>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
