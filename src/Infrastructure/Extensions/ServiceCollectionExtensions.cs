using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Application.Interfaces.Services.Storage;
using VoteApp.Application.Interfaces.Services.Storage.Provider;
using VoteApp.Application.Interfaces.Serialization.Serializers;
using VoteApp.Application.Serialization.JsonConverters;
using VoteApp.Infrastructure.Repositories;
using VoteApp.Infrastructure.Services.Storage;
using VoteApp.Application.Serialization.Options;
using VoteApp.Infrastructure.Services.Storage.Provider;
using VoteApp.Application.Serialization.Serializers;
using MediatR;
using VoteApp.Infrastructure.Services.Dweet;
using VoteApp.Application.Interfaces.Services;
using VoteApp.Infrastructure.Services.Rabbit;

namespace VoteApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IDweetService, DweetService>();
            services.AddSingleton<IMessageService, RabbitService>();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        public static IServiceCollection AddServerStorage(this IServiceCollection services)
            => AddServerStorage(services, null);

        public static IServiceCollection AddServerStorage(this IServiceCollection services, Action<SystemTextJsonOptions> configure)
        {
            return services
                .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
                .AddScoped<IStorageProvider, ServerStorageProvider>()
                .AddScoped<IServerStorageService, ServerStorageService>()
                .AddScoped<ISyncServerStorageService, ServerStorageService>()
                .Configure<SystemTextJsonOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                        configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }
    }
}