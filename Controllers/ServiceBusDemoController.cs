using Azure.Messaging.ServiceBus;
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
    public class ServiceBusDemoController : ControllerBase
    {
        private static string connection_string = "Endpoint=sb://servicebus240122.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=3bfxLGDA/w0VXNkOTMt//mk756sUmiplTd/em3ie/kM=;EntityPath=app_queue";
        private static string queue_name = "app_queue";

        public ServiceBusDemoController()
        {

        }

        [HttpPost("queue/message")]
        public async Task PostMessageInQuiue([FromBody] Order order)
        {
            ServiceBusClient serviceBusClient = new ServiceBusClient(connection_string);
            ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(queue_name);
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(order.ToString());

            await serviceBusSender.SendMessageAsync(serviceBusMessage);
        }
    }
}
