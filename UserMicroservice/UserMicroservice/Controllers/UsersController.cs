using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserMicroservice.DataPresentation.Models;
using UserMicroservice.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserMicroservice.Controllers
{
     [Route("api/users")]
     public class UsersController : Controller
     {
          private readonly ILogger _logger;
          private readonly IUserService _userService;

          public UsersController(IUserService userService, ILogger<UsersController> logger)
          {
               _userService = userService;
               _logger = logger;
          }

          // GET: api/<controller>
          [HttpGet]
          public IActionResult Get()
          {
               _logger.LogInformation("Start getting users");

               var users = _userService.GetUsers();

               return Ok(users);
          }

          // POST api/<controller>
          [HttpPost]
          public void Post([FromBody]string value)
          {
          }
     }
}
