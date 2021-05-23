namespace QueueService.Configuration
{
    public enum ProviderType
    {
        AzureStorageQueue = 0,
        AzureServiceBus = 1,

        // Not supported ATM
        // but added since it was mentioned in task
        AzureEventHub = 2,
        AmazonSQS = 3,
        ApacheKafka = 4,
        RabbitMQ = 5
    }
}
