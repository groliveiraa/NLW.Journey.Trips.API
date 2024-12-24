using Journey.Application.Interfaces;
using Journey.Application.Services;
using Journey.Domain;
using Journey.Domain.Interfaces.Repositories;
using Journey.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Journey.Infrastructure.Extensions
{
    public static class DI
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<JourneyDbContext>(options =>
                options.UseSqlite(connectionString));
        }

        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            // Configuração dos repositórios
            services.AddScoped<ITripRepository, TripRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();

            // Configuração dos serviços
            services.AddScoped<ITripService, TripService>();
            services.AddScoped<IActivityService, ActivityService>();

            return services;

        }
    }
}
