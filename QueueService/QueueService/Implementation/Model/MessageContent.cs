using QueueService.Interface.Model;
using System;

namespace QueueService.Implementation.Model
{
    internal class MessageContent<T> : IMessageContent<T>
    {
        #region Properties

        public Guid Id { get; set; }

        public string Type { get; }

        public T Data { get; set; }

        #endregion Properties

        #region Constructors

        internal MessageContent(
            string type)
        {
            Id = Guid.NewGuid();
            Type = type;
        }

        #endregion Constructors
    }
}
