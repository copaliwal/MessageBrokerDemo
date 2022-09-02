using Azure.Storage.Queues;
using MessageBrokerDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace MessageBrokerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageAccountDemoController : ControllerBase
    {
        readonly string StorageAccount_QueueConnection;
        readonly string StorageAccount_QueueName;

        public StorageAccountDemoController(IConfiguration configuration)
        {
            StorageAccount_QueueConnection = configuration.GetSection("ServiceBusQueue")["ConnectionString"];
            StorageAccount_QueueName = configuration.GetSection("ServiceBusQueue")["QueueName"];
        }

        [HttpPost("queue/message")]
        public async Task PostMessageInQuiue([FromBody] dynamic message)
        {
            QueueClient queueClient = new(StorageAccount_QueueConnection, StorageAccount_QueueName);
            var textBytes = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            await queueClient.SendMessageAsync(Convert.ToBase64String(textBytes));
        }
    }
}
