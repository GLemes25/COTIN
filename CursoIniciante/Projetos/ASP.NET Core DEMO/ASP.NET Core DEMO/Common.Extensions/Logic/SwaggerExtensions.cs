using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Extensions.Logic
{
    public static class SwaggerExtensions //onde eu consigo passar informações
    {
        public static void AddXmlComments(this SwaggerGenOptions options)
        {
            var basePath = AppContext.BaseDirectory;
            var assemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            var fileName = Path.GetFileName(assemblyName + ".xml");
            var xmlDocumentPath = Path.Combine(basePath, fileName);

            if (File.Exists(xmlDocumentPath))
            {
                options.IncludeXmlComments(xmlDocumentPath);
            };

        }
    }
}
