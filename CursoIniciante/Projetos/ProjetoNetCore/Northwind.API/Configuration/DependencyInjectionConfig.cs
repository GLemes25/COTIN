using Common.Api.Logic.Business.Intefaces;
using Common.Api.Logic.Business.Notification;
using EntityFrameworkCore.UseRowNumberForPaging;
using Microsoft.EntityFrameworkCore;
using Northwind.API.Logic.Interfaces;
using Northwind.API.Logic.Services;
using Northwind.Data.Logic.Data.Northwind.Context;
using System.Reflection;
using Common.UnitOfWork;

namespace Northwind.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NorthWindContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("NorthwindConnection"), builder => builder.UseRowNumberForPaging());
            }, ServiceLifetime.Scoped);

            services.AddUnitOfWork<NorthWindContext>();

            services.AddScoped<INotification, Notification>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddHealthChecks();

            services.AddScoped<ICategoriesServices, CategoriesServices>();

            services.AddScoped<IProductsServices, ProdutcsServices>();

            services.AddScoped<ISuppliersServices, SuppliersServices>();



        }
    }
}
