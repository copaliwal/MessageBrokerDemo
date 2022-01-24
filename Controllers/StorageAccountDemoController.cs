using Azure.Storage.Queues;
using MessageBrokerDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBrokerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageAccountDemoController : ControllerBase
    {
        [HttpPost("queue/message")]
        public async Task PostMessageInQuiue([FromBody] Order order)
        {
            string connection_string = "DefaultEndpointsProtocol=https;AccountName=storageaccount240122;AccountKey=n9w26cg5xfZsdbMG0/ALLtsWF72kg0l7aGkFw/X8tM+0/XIYAfO05wvWXn6fOInOd4aQEvclhLMSxiUvzocWUQ==;EndpointSuffix=core.windows.net";
            string queue_name = "appqueue";

            QueueClient queueClient = new QueueClient(connection_string, queue_name);
            var textBytes = System.Text.Encoding.UTF8.GetBytes(order.ToString());
            await queueClient.SendMessageAsync(System.Convert.ToBase64String(textBytes));
        }
    }
}
