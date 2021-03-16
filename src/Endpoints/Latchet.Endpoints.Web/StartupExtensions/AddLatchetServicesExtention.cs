using Latchet.Infrastructures.Events.Outbox;
using Latchet.Infrastructures.Messaging.IdempotentConsumers;
using Latchet.Infrastructures.Tools.Caching.Microsoft;
using Latchet.Infrastructures.Tools.ObjectMapper.AutoMapper.DependencyInjection;
using Latchet.InfrastructuresEvents.PullingPublisher;
using Latchet.Persistence.ChangeInterceptors.EntityChageInterceptorItems;
using Latchet.Utilities;
using Latchet.Utilities.Configurations;
using Latchet.Utilities.Services.Caching;
using Latchet.Utilities.Services.JsonSerializers;
using Latchet.Utilities.Services.Logger;
using Latchet.Utilities.Services.MessageBus;
using Latchet.Utilities.Services.Translations;
using Latchet.Utilities.Services.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Endpoints.Web.StartupExtensions
{
    public static class AddLatchetServicesExtentions
    {
        public static IServiceCollection AddLatchetServices(
            this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            services.AddCaching();
            services.AddLogging();
            services.AddJsonSerializer(assembliesForSearch);
            services.AddObjectMapper(assembliesForSearch);
            services.AddUserInfoService(assembliesForSearch);
            services.AddTranslator(assembliesForSearch);
            services.AddMessageBus(assembliesForSearch);
            services.AddPoolingPublisher(assembliesForSearch);
            services.AddTransient<LatchetServices>();
            services.AddEntityChangeInterception(assembliesForSearch);
            return services;
        }

        private static IServiceCollection AddCaching(this IServiceCollection services)
        {
            var latchetConfigurations = services.BuildServiceProvider().GetService<LatchetConfiguration>();
            if (latchetConfigurations?.Caching?.Enable == true)
            {
                if (latchetConfigurations.Caching.Provider == ChachProvider.MemoryCache)
                {
                    services.AddScoped<ICacheAdapter, InMemoryCacheAdapter>();
                }
                else
                {
                    services.AddScoped<ICacheAdapter, DistributedCacheAdapter>();
                }

                switch (latchetConfigurations.Caching.Provider)
                {
                    case ChachProvider.DistributedSqlServerCache:
                        services.AddDistributedSqlServerCache(options =>
                        {
                            options.ConnectionString = latchetConfigurations.Caching.DistributedSqlServerCache.ConnectionString;
                            options.SchemaName = latchetConfigurations.Caching.DistributedSqlServerCache.SchemaName;
                            options.TableName = latchetConfigurations.Caching.DistributedSqlServerCache.TableName;
                        });
                        break;
                    case ChachProvider.StackExchangeRedisCache:
                        services.AddStackExchangeRedisCache(options =>
                        {
                            options.Configuration = latchetConfigurations.Caching.StackExchangeRedisCache.Configuration;
                            options.InstanceName = latchetConfigurations.Caching.StackExchangeRedisCache.SampleInstance;
                        });
                        break;
                    case ChachProvider.NCacheDistributedCache:
                        throw new NotSupportedException("NCache Not Supporting yet");
                    default:
                        services.AddMemoryCache();
                        break;
                }
            }
            else
            {
                services.AddScoped<ICacheAdapter, NullObjectCacheAdapter>();
            }
            return services;
        }

        private static IServiceCollection AddLogging(this IServiceCollection services)
        {
            return services.AddScoped<IScopeInformation, ScopeInformation>();
        }

        public static IServiceCollection AddJsonSerializer(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        {
            var latchingConfigurations = services.BuildServiceProvider().GetService<LatchetConfiguration>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(c => c.Where(type => type.Name == latchingConfigurations.JsonSerializerTypeName && typeof(IJsonSerializer).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }


        private static IServiceCollection AddObjectMapper(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        {
            var latchetConfigurations = services.BuildServiceProvider().GetService<LatchetConfiguration>();
            if (latchetConfigurations.RegisterAutomapperProfiles)
            {
                services.AddAutoMapperProfiles(assembliesForSearch);
            }
            return services;
        }
        private static IServiceCollection AddUserInfoService(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var latchetConfigurations = services.BuildServiceProvider().GetService<LatchetConfiguration>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == latchetConfigurations.UserInfoServiceTypeName && typeof(IUserInfoService).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

            return services;
        }
        private static IServiceCollection AddTranslator(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var latchetConfigurations = services.BuildServiceProvider().GetService<LatchetConfiguration>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == latchetConfigurations.Translator.TranslatorTypeName && typeof(ITranslator).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }


        private static IServiceCollection AddMessageBus(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var latchetConfigurations = services.BuildServiceProvider().GetService<LatchetConfiguration>();
            if (latchetConfigurations.MessageBus.Enabled)
            {
                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == latchetConfigurations.MessageBus.MessageConsumerTypeName && typeof(IMessageConsumer).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                 .AddClasses(classes => classes.Where(type => type.Name == latchetConfigurations.Messageconsumer.MessageInboxStoreTypeName && typeof(IMessageInboxItemRepository).IsAssignableFrom(type)))
                 .AsImplementedInterfaces()
                 .WithTransientLifetime());

                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                    .AddClasses(classes => classes.Where(type => type.Name == latchetConfigurations.MessageBus.MessageBusTypeName && typeof(IMessageBus).IsAssignableFrom(type)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());
            }
            return services;
        }

        private static IServiceCollection AddPoolingPublisher(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var latchetConfigurations = services.BuildServiceProvider().GetService<LatchetConfiguration>();
            if (latchetConfigurations.PullingPublisher.Enabled)
            {
                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                    .AddClasses(classes => classes.Where(type => type.Name == latchetConfigurations.PullingPublisher.OutBoxRepositoryTypeName && typeof(IOutBoxEventItemRepository).IsAssignableFrom(type)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());
                services.AddHostedService<PoolingPublisherHostedService>();

            }
            return services;
        }

        private static IServiceCollection AddEntityChangeInterception(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var latchetConfigurations = services.BuildServiceProvider().GetService<LatchetConfiguration>();
            if (latchetConfigurations.EntityChangeInterception.Enabled)
            {
                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                    .AddClasses(classes => classes.Where(type => type.Name == latchetConfigurations.EntityChangeInterception.
                        EntityChageInterceptorRepositoryTypeName && typeof(IEntityChageInterceptorItemRepository).IsAssignableFrom(type)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
            }
            return services;
        }
    }
}
