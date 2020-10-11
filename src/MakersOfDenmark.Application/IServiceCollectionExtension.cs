namespace MakersOfDenmark.Application
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;
    using MediatR;

    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServiceDependencies(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
