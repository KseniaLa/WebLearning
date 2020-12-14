using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using WebLearning.DataPresentation.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebLearning.Controllers
{
     [Route("api/users")]
     public class UsersController : Controller
     {
          // GET: api/users
          [HttpGet]
          public IActionResult GetUser()
          {
               var user = new User { Name = "Bob", Age = 21 };
               
               return Ok(user);
          }

          // GET api/users/item
          [HttpGet("item")]
          public IActionResult GetItem()
          {
               var user = new Item { Id = "123", Name = "Test" };

               return Ok(user);
          }

          // POST api/<controller>
          [HttpPost]
          public void Post([FromBody]string value)
          {
          }

          // PUT api/<controller>/5
          [HttpPut("{id}")]
          public void Put(int id, [FromBody]string value)
          {
          }

          // DELETE api/<controller>/5
          [HttpDelete("{id}")]
          public void Delete(int id)
          {
          }
     }
}
