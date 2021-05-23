using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class MessageModel
    {
        #region Properties

        [Required]
        public string To { get; set; }

        [Required]
        public string Text { get; set; }

        #endregion Properties
    }
}
