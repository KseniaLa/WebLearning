using Common.Messaging;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserMicroservice.Messaging.AzureServiceBus.Configuration;

namespace UserMicroservice.Messaging.AzureServiceBus.Consuming
{
     public class ServiceBusConsumer : IServiceBusConsumer
     {
          private readonly ITaskProcessor _dataProcessor;
          private readonly QueueClient _queueClient;
          private readonly ILogger _logger;

          public ServiceBusConsumer(ITaskProcessor taskProcessor,
              IOptions<AzureServiceBusConfiguration> asureServiceBusOptions,
              ILogger<ServiceBusConsumer> logger)
          {
               _dataProcessor = taskProcessor;
               _logger = logger;

               var connectionString = asureServiceBusOptions.Value.ConnectionString;
               var queueName = asureServiceBusOptions.Value.QueueName;
               _queueClient = new QueueClient(connectionString, queueName);
          }

          public void RegisterOnMessageHandlerAndReceiveMessages()
          {
               var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
               {
                    MaxConcurrentCalls = 1,
                    AutoComplete = false
               };

               _queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
          }

          private async Task ProcessMessagesAsync(Message message, CancellationToken token)
          {
               _logger.LogInformation("Start message processing");

               var taskPayload = JsonConvert.DeserializeObject<TaskAssignedMessage>(Encoding.UTF8.GetString(message.Body));
               _dataProcessor.Process(taskPayload);
               await _queueClient.CompleteAsync(message.SystemProperties.LockToken).ConfigureAwait(false);

               _logger.LogInformation("Finish message processing");
          }

          private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
          {
               _logger.LogError(exceptionReceivedEventArgs.Exception, "Message handler encountered an exception");
               var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

               _logger.LogDebug($"- Endpoint: {context.Endpoint}");
               _logger.LogDebug($"- Entity Path: {context.EntityPath}");
               _logger.LogDebug($"- Executing Action: {context.Action}");

               return Task.CompletedTask;
          }

          public async Task CloseQueueAsync()
          {
               await _queueClient.CloseAsync().ConfigureAwait(false);
          }
     }
}
