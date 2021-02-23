using Common.Messaging;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskMicroservice.Messaging.AzureServiceBus.Configuration;

namespace TaskMicroservice.Messaging.AzureServiceBus.Publishing
{
     public class ServiceBusSender : IServiceBusSender
     {
          private readonly QueueClient _queueClient;

          public ServiceBusSender(IOptions<AzureServiceBusConfiguration> asureServiceBusOptions)
          {
               var connectionString = asureServiceBusOptions.Value.ConnectionString;
               var queueName = asureServiceBusOptions.Value.QueueName;
               _queueClient = new QueueClient(connectionString, queueName);
          }

          public async Task SendMessage(TaskAssignedMessage payload)
          {
               var data = JsonConvert.SerializeObject(payload);
               var message = new Message(Encoding.UTF8.GetBytes(data));

               await _queueClient.SendAsync(message);
          }
     }
}
