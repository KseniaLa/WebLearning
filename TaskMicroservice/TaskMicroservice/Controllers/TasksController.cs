using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Common.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskMicroservice.DataPresentation.Models;
using TaskMicroservice.Messaging.AzureServiceBus.Publishing;
using TaskMicroservice.Messaging.RabbitMQ.Publishing;
using TaskMicroservice.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskMicroservice.Controllers
{
     [Route("api/tasks")]
     public class TasksController : Controller
     {
          private readonly ILogger _logger;
          private readonly ITaskService _taskService;
          private readonly ITaskSender _taskSender;
          private readonly IServiceBusSender _serviceBusSender;

          public TasksController(ITaskService taskService, ITaskSender taskSender, IServiceBusSender serviceBusSender, ILogger<TasksController> logger) 
          {
               _taskService = taskService;
               _taskSender = taskSender;
               _serviceBusSender = serviceBusSender;
               _logger = logger;
          }

          [HttpGet]
          public IActionResult GetTasks()
          {
               _logger.LogInformation("Start tasks processing");

               var tasks = _taskService.GetTasks();

               return Ok(tasks);
          }

          [HttpPost]
          public async Task<IActionResult> AddTask([FromBody]WorkTask task)
          {
               var taskAssignedMessage = new TaskAssignedMessage { TaskId = task.Id, TaskName = task.Title, AssignedByUserId = task.AssignedByUserId, AssignedToUserId = task.AssignedToUserId };
               
               _taskSender.SendMessage(taskAssignedMessage);
               await _serviceBusSender.SendMessage(taskAssignedMessage);

               return Ok();
          }
     }
}
