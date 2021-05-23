using Azure.Storage.Queues;
using Microsoft.Extensions.Options;
using QueueService.Configuration;
using QueueService.Implementation.Service.Base;
using QueueService.Interface.Model;
using QueueService.Interface.Service;
using System.Text.Json;
using System.Threading.Tasks;

namespace QueueService.Implementation.Service
{
    public class AzureStorageQueueService : QueueServiceBase, IQueueService
    {
        #region Fields

        private readonly QueueClient Client;

        #endregion Fields

        #region Constructors

        public AzureStorageQueueService(
            IOptions<QueueServiceConfiguration> options,
            QueueClient queueClient)
            : base(
                options)
        {
            Client = queueClient;
        }

        #endregion Constructors

        #region Methods

        public async Task SendAsync<T>(
            IMessageContent<T> messageContent)
        {
            var message = JsonSerializer.Serialize(messageContent);
            await Client.SendMessageAsync(message);
        }

        public async Task Dequeue()
        {
            var message = await Client.ReceiveMessageAsync();
            if (message?.Value != null)
            {
                await Client.DeleteMessageAsync(message.Value.MessageId, message.Value.PopReceipt);
            }
        }

        #endregion Methods
    }
}
