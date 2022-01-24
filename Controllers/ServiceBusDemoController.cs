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
        public ServiceBusDemoController()
        {

        }

        [HttpPost("queue/message")]
        public async Task PostMessageInQuiue([FromBody] Order order)
        {
            string connection_string = "Endpoint=sb://servicebus240122.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=3bfxLGDA/w0VXNkOTMt//mk756sUmiplTd/em3ie/kM=;EntityPath=app_queue";
            string queue_name = "app_queue";
            ServiceBusClient serviceBusClient = new ServiceBusClient(connection_string);
            ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(queue_name);
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(order.ToString());

            await serviceBusSender.SendMessageAsync(serviceBusMessage);
        }

        [HttpPost("topic/message")]
        public async Task PostMessageInTopic([FromBody] Order order)
        {
            string connection_string = "Endpoint=sb://servicebus240122.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=PFjynsOrUOrEI943WnRo5IqScHQ6cbnCJbvkM6qSYTA=;EntityPath=app_topic";
            string topic_name = "app_topic";
            ServiceBusClient serviceBusClient = new ServiceBusClient(connection_string);
            ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(topic_name);
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(order.ToString());

            await serviceBusSender.SendMessageAsync(serviceBusMessage);
        }
    }
}
