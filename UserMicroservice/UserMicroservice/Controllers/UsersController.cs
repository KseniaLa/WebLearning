using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.DataPresentation.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserMicroservice.Controllers
{
     [Route("api/users")]
     public class UsersController : Controller
     {
          // GET: api/<controller>
          [HttpGet]
          public IActionResult Get()
          {
               var users = new List<User>
               {
                    new User { Id = 1, Name = "Bob" },
                    new User { Id = 2, Name = "Ann" },
               };

               var item = new Item { Id = "www", Name = "Ben" };

               users.Add(new User
               {
                    Id = 3,
                    Name = item.Name
               });

               return Ok(users);
          }

          // POST api/<controller>
          [HttpPost]
          public void Post([FromBody]string value)
          {
          }
     }
}
