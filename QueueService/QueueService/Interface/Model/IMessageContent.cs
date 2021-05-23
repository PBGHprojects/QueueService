using System;

namespace QueueService.Interface.Model
{
    // Wasn't sure if I should create strongly typed 'Data' depending on 'MessageType'
    // which would then require implementing something like IQueueService<TMessageType>
    
    // On call we discussed that 'MessageType' would be part of 'QueueName' or topic maybe
    // but seemed a bit silly having already 'QueueName'
    // and if I understand correctly - AzureStorageQueue doesn't support topics
    
    // So adding 'Type' to this model seemed like the best solution (given time and requirements)
    // assuming project would use model(s)
    // which whatever processor reads this messages would know how to handle based on 'Type'

    public interface IMessageContent<T>
    {
        #region Properties

        // Left public setter assuming other projects should have
        // the ability to change it
        Guid Id { get; set; }

        string Type { get; }

        T Data { get; set; }

        #endregion Properties
    }
}
