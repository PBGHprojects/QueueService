using Microsoft.Extensions.Options;
using QueueService.Configuration;
using QueueService.Implementation.Model;
using QueueService.Interface.Model;

namespace QueueService.Implementation.Service.Base
{
    public abstract class QueueServiceBase
    {
        #region Fields

        protected readonly QueueServiceConfiguration Configuration;

        #endregion Fields

        #region Constructors

        protected QueueServiceBase(
            IOptions<QueueServiceConfiguration> options)
        {
            Configuration = options.Value;
        }

        #endregion Constructors

        #region Methods

        public IMessageContent<T> CreateMessage<T>()
        {
            return new MessageContent<T>(Configuration.MessageType);
        }

        public IMessageContent<T> CreateMessage<T>(
            T messageData)
        {
            var result = CreateMessage<T>();
            result.Data = messageData;
            return result;
        }

        #endregion Methods
    }
}
