using Common.Messaging;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
          private readonly string _connectionString;
          private readonly string _queueName;
          private readonly ILogger _logger;
          private QueueClient _queueClient;

          public ServiceBusSender(IOptions<AzureServiceBusConfiguration> asureServiceBusOptions, ILogger<ServiceBusSender> logger)
          {
               _connectionString = asureServiceBusOptions.Value.ConnectionString;
               _queueName = asureServiceBusOptions.Value.QueueName;
               _logger = logger;

               CreateConnection();
          }

          private void CreateConnection()
          {
               try
               {
                    _queueClient = new QueueClient(_connectionString, _queueName);
               }
               catch (Exception ex)
               {
                    _logger.LogError($"Could not create connection: {ex.Message}");
               }
          }

          private bool ConnectionExists()
          {
               if (_queueClient != null)
               {
                    return true;
               }

               CreateConnection();

               return _queueClient != null;
          }

          public async Task SendMessage(TaskAssignedMessage payload)
          {
               if (ConnectionExists())
               {
                    var data = JsonConvert.SerializeObject(payload);
                    var message = new Message(Encoding.UTF8.GetBytes(data));

                    await _queueClient.SendAsync(message);
               }
          }
     }
}
