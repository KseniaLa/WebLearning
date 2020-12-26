using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using TaskMicroservice.DataPresentation.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskMicroservice.Controllers
{
     [Route("api/tasks")]
     public class TasksController : Controller
     {
          // GET: api/<controller>
          [HttpGet]
          public IActionResult Get()
          {
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

               return Ok(tasks);
          }

          // POST api/<controller>
          [HttpPost]
          public void Post([FromBody]string value)
          {
          }
     }
}
