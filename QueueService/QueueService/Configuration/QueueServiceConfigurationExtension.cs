using Azure.Messaging.ServiceBus;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueueService.Implementation.Service;
using QueueService.Interface.Service;
using System;
using System.ComponentModel.DataAnnotations;

namespace QueueService.Configuration
{
    public static class QueueServiceConfigurationExtension
    {
        #region Methods

        public static IServiceCollection AddQueueService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var queueServiceConfigurationSection = configuration
                .GetSection(QueueServiceConfiguration.POSITION);

            var queueServiceConfiguration = new QueueServiceConfiguration();

            queueServiceConfigurationSection
                .Bind(queueServiceConfiguration);

            queueServiceConfiguration.Validate();

            services
                .AddOptions<QueueServiceConfiguration>()
                .Bind(queueServiceConfigurationSection);

            switch (queueServiceConfiguration.Provider)
            {
                case ProviderType.AzureStorageQueue:
                    services.AddSingleton<IQueueService, AzureStorageQueueService>();
                    services.AddSingleton(
                        new QueueClient(
                            queueServiceConfiguration.ConnectionString,
                            queueServiceConfiguration.QueueName));
                    break;

                case ProviderType.AzureServiceBus:
                    services.AddSingleton<IQueueService, AzureServiceBusService>();
                    services.AddSingleton(
                        new ServiceBusClient(
                            queueServiceConfiguration.ConnectionString));
                    break;

                default:
                    throw new NotImplementedException();
            }


            return services;
        }

        private static void Validate(
            this QueueServiceConfiguration configuration)
        {
            // Why not .ValidateDataAnnotations()?
            // They are only called when options are requested from service collection
            // (upon requesting dependency)
            // while this will instanlty validate configuration
            // (in this case on startup)

            Validator.ValidateObject(configuration, new ValidationContext(configuration));
        }

        #endregion Methods
    }
}
