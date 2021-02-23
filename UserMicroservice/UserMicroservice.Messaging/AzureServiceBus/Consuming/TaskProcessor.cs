using Common.Messaging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserMicroservice.Messaging.AzureServiceBus.Consuming
{
     public class TaskProcessor : ITaskProcessor
     {
          private readonly ILogger _logger;

          public TaskProcessor(ILogger<TaskProcessor> logger)
          {
               _logger = logger;
          }

          public void Process(TaskAssignedMessage task)
          {
               _logger.LogInformation($"Process Azure message: {task.TaskName}");
          }
     }
}
