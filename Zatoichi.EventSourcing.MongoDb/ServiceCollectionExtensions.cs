namespace Zatoichi.EventSourcing.MongoDb
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoEventStore(IServiceCollection services)
        {
            services.AddTransient<IEventStore, MongoEventStore>();
            return services;
        }
    }
}