namespace Zatoichi.EventSourcing
{
    using Commands;
    using Microsoft.Extensions.DependencyInjection;
    using Queries;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventSourcing(this IServiceCollection services)
        {
            services.AddTransient<IQueryBus, QueryBus>()
                .AddTransient<ICommandBus, CommandBus>();

            return services;
        }
    }
}