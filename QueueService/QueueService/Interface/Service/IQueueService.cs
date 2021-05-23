using QueueService.Interface.Model;
using System.Threading.Tasks;

namespace QueueService.Interface.Service
{
    public interface IQueueService
    {
        #region Methods

        IMessageContent<T> CreateMessage<T>();

        IMessageContent<T> CreateMessage<T>(
            T messageData);

        Task SendAsync<T>(
            IMessageContent<T> messageContent);

        // Was not specified in task
        // but added it as it was mentioned on call
        Task Dequeue();

        #endregion Methods
    }
}
