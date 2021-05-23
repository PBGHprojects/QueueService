using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using QueueService.Configuration;
using QueueService.Implementation.Service.Base;
using QueueService.Interface.Model;
using QueueService.Interface.Service;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QueueService.Implementation.Service
{
    public class AzureServiceBusService : QueueServiceBase, IQueueService
    {
        #region Fields

        private readonly TimeSpan DefaultReceiveTime = TimeSpan.FromSeconds(1);

        private readonly ServiceBusSender Sender;

        private readonly ServiceBusReceiver Receiver;

        #endregion Fields

        #region Constructors

        public AzureServiceBusService(
            IOptions<QueueServiceConfiguration> options,
            ServiceBusClient serviceBusClient)
            : base(
                options)
        {
            Sender = serviceBusClient.CreateSender(Configuration.QueueName);
            Receiver = serviceBusClient.CreateReceiver(Configuration.QueueName);
        }

        #endregion Constructors

        #region Methods

        public async Task SendAsync<T>(
            IMessageContent<T> messageContent)
        {
            var messageBody = JsonSerializer.Serialize(messageContent);
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));
            await Sender.SendMessageAsync(message);
        }

        public async Task Dequeue()
        {
            var message = await Receiver.ReceiveMessageAsync(DefaultReceiveTime);
            if (message != null)
            {
                await Receiver.CompleteMessageAsync(message);
            }
        }

        #endregion Methods
    }
}
