using Common.Extensions.Logic;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Northwind.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            // Ele é um método de extensão que adiciona endpoints mínimos sejam adicionados ao API explorer
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //  CORS (Cross Origin Resource Sharing) Compartilhamento de Recursos de Origem Cruzada 
            //  Permitir qualquer Header, Method, Credentials e Origins
            //https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0
            string[] policys = configuration["Origins"].Split(",");
            services.AddCors(opt => opt.AddPolicy("CorsPolicy",

            builder => //bloqueio
            {
                builder
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .WithOrigins(policys);
            }));
        }



        public static void UseApiConfiguration(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseMiddleware<ExceptionMiddleware>();

            var cultureInfo = new CultureInfo("pt-BR");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            var supportedCultures = new[] { cultureInfo };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
        }
    }
}

