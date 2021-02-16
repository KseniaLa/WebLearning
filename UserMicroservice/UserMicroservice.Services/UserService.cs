using Common.DependencyInjection;
using Common.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using UserMicroservice.DataPresentation.Models;
using UserMicroservice.Services.Interfaces;

namespace UserMicroservice.Services
{
     public class UserService : IUserService, IScopedService
     {
          private readonly ILogger _logger;

          public UserService(ILogger<UserService> logger)
          {
               _logger = logger;
          }

          public List<User> GetUsers()
          {
               _logger.LogInformation("Start service users processing");

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

               return users;
          }
     }
}
