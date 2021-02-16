using System;
using System.Collections.Generic;
using System.Text;
using UserMicroservice.DataPresentation.Models;

namespace UserMicroservice.Services.Interfaces
{
     public interface IUserService
     {
          List<User> GetUsers();
     }
}
