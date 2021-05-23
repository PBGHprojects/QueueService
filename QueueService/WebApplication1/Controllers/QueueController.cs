using Microsoft.AspNetCore.Mvc;
using QueueService.Interface.Service;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/queues")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        #region Fields

        private readonly IQueueService Service;

        #endregion Fields

        #region Constructors

        public QueueController(
            IQueueService queueService)
        {
            Service = queueService;
        }

        #endregion Constructors

        #region Methods

        [HttpPost("/create")]
        public async Task Post(
            [FromBody] MessageModel model)
        {
            // Wouldn't usually use view model for creating messages
            // Using it here only because this is a 'dummy' project
            var message = Service.CreateMessage(model);
            await Service.SendAsync(message);
        }

        [HttpDelete("/dequeue")]
        public async Task Delete()
        {
            await Service.Dequeue();
        }

        #endregion Methods
    }
}
