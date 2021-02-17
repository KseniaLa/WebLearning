using Common.DependencyInjection;
using Common.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TaskMicroservice.DataPresentation.Models;
using TaskMicroservice.Services.Interfaces;

namespace TaskMicroservice.Services
{
     public class TaskService : ITaskService, IScopedService
     {
          private readonly ILogger _logger;

          public TaskService(ILogger<TaskService> logger)
          {
               _logger = logger;
          }

          public List<WorkTask> GetTasks()
          {
               _logger.LogInformation("Start service tasks processing");

               var tasks = new List<WorkTask>
               {
                    new WorkTask { Id = 1, Title = "Task1" },
                    new WorkTask { Id = 2, Title = "Task2" },
                    new WorkTask { Id = 3, Title = "Task3" },
               };

               var item = new Item { Id = "www", Name = "Item4" };

               tasks.Add(new WorkTask
               {
                    Id = 4,
                    Title = $"{item.Name} - {item.Id}"
               });

               return tasks;
          }
     }
}
