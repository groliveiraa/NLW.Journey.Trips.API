using Microsoft.OpenApi.Models;

namespace Journey.Api.Swagger
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(d =>
            {
                d.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NLWJourney.Trips.API",
                    Version = "v1",
                    Description = "Rocketseat - NLW Journey: API gerenciamento de viagens",
                    Contact = new OpenApiContact
                    {
                        Name = "NLW Journey - Rocketseat",
                    }
                });

                var xmlFile = "Journey.API.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                d.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
