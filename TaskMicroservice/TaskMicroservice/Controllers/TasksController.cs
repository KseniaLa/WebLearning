using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskMicroservice.DataPresentation.Models;
using TaskMicroservice.Messaging.Publishing;
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

          public TasksController(ITaskService taskService, ITaskSender taskSender, ILogger<TasksController> logger) 
          {
               _taskService = taskService;
               _taskSender = taskSender;
               _logger = logger;
          }

          // GET: api/<controller>
          [HttpGet]
          public IActionResult Get()
          {
               _logger.LogInformation("Start tasks processing");

               var tasks = _taskService.GetTasks();

               _taskSender.SendMessage(new WorkTask { Id = 10, Title = "RabbitMQ task" });

               return Ok(tasks);
          }

          // POST api/<controller>
          [HttpPost]
          public void Post([FromBody]string value)
          {
          }
     }
}
