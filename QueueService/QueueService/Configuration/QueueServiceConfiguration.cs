using System.ComponentModel.DataAnnotations;

namespace QueueService.Configuration
{
    public class QueueServiceConfiguration
    {
        #region Constants

        public const string POSITION = "QueueService";

        #endregion Constants

        #region Properties

        [Required]
        public ProviderType Provider { get; set; }

        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string QueueName { get; set; }

        [Required]
        public string MessageType { get; set; }

        #endregion Properties
    }
}
