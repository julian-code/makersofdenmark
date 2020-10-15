namespace MakersOfDenmark.Infrastructure
{
    using MakersOfDenmark.Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MODContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("MakersOfDenmark.Infrastructure")));
            return services;
        }

        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks().AddDbContextCheck<MODContext>();

            return services;
        }
    }

    public class HealthCheckResponse
    {
        public string Status { get; set; }
        public IEnumerable<HealtCheck> Checks { get; set; }
        public TimeSpan Duration { get; set; }
    }

    public class HealtCheck
    {
        public string Status { get; set; }
        public string Component { get; set; }
        public string Description { get; set; }

    }
}
