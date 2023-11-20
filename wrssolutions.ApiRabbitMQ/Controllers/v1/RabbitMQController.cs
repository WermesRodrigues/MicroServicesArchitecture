using wrssolutions.Configs;
using wrssolutions.DTO.Dto;
using wrssolutions.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace wrssolutions.ApiRabbitMQ.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class RabbitMQController : ControllerBase
    {
        private readonly ISvcRabbitMQ _svcRabbitMQ;

        public RabbitMQController(ISvcRabbitMQ svcRabbitMQ)
        {
            _svcRabbitMQ = svcRabbitMQ;
        }

        // POST: 
        [HttpPost]
        public IActionResult Post([FromBody] dtoMessagesRabbitMQ messagesRabbitMQ)
        {
            if (_svcRabbitMQ.BasicPublish(Settings.RabbitMQEventMessages, messagesRabbitMQ))
            {
                return Accepted();
            }

            return BadRequest();
        }
    }
}
