using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Threading.Tasks;

namespace MessageBrokerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceBusDemoController : ControllerBase
    {
        private IConfiguration _configuration;

        public ServiceBusDemoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("queue/message")]
        public async Task PostMessageInQuiue([FromBody] dynamic queueMessage)
        {
            var connectionString = _configuration.GetSection("ServiceBusQueue")["ConnectionString"];
            var queueName = _configuration.GetSection("ServiceBusQueue")["QueueName"];

            ServiceBusClient serviceBusClient = new(connectionString);
            ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(queueName);
            ServiceBusMessage serviceBusMessage = new(JsonSerializer.Serialize(queueMessage));

            await serviceBusSender.SendMessageAsync(serviceBusMessage);
        }

        [HttpPost("topic/message")]
        public async Task PostMessageInTopic([FromBody] dynamic queueMessage)
        {
            var connectionString = _configuration.GetSection("ServiceBusTopic")["ConnectionString"];
            var topicName = _configuration.GetSection("ServiceBusTopic")["TopicName"];

            ServiceBusClient serviceBusClient = new(connectionString);
            ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(topicName);
            ServiceBusMessage serviceBusMessage = new(JsonSerializer.Serialize(queueMessage));

            await serviceBusSender.SendMessageAsync(serviceBusMessage);
        }
    }
}
