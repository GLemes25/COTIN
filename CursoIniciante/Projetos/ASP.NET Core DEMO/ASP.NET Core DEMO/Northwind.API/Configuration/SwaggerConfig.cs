using Common.Extensions.Logic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Northwind.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var buildingVersion = configuration["AppSettings:Build"].ToString();
            var descriptionAPI = configuration["AppSettings:Description"].ToString();

            services.AddApiVersioning(
              options =>
              {
                  options.DefaultApiVersion = new ApiVersion(1, 0);
                  options.ReportApiVersions = true;
                  options.AssumeDefaultVersionWhenUnspecified = true;
              });

            services.AddVersionedApiExplorer(
              options =>
              {
                  options.GroupNameFormat = "'v'VVV";
                  options.SubstituteApiVersionInUrl = true;
              });

            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {

                    c.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Version = description.ApiVersion.ToString(),
                        Title = $"{descriptionAPI} - OnContainers",
                        Description = $"Application with Swagger and API versioning - Version {buildingVersion}",
                        Contact = new OpenApiContact() { Name = "Vivian Lemes", Email = "vverissimo@Fazenda.ms.gov.br" },
                        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                    });
                }
                c.OperationFilter<SwaggerDefaultValues>();
                c.AddXmlComments();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                           Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    },
                                    Scheme = "oauth2",
                                    Name = "Bearer",
                                    In = ParameterLocation.Header,

                        },
                        new string[] {}
                    }
                });
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app,
                                                        IWebHostEnvironment env,
                                                        IApiVersionDescriptionProvider provider,
                                                        IConfiguration configuration)
        {
            var appName = configuration["AppSettings:AppName"].ToString();
            var descriptionAPI = configuration["AppSettings:Description"].ToString();

            app.UseSwagger();
            app.UseSwaggerUI(
            options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    var version = string.Format("{0} {1}", descriptionAPI, description.GroupName);
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", version);
                    options.DocumentTitle = appName;
                }
            });
        }
    }
}
