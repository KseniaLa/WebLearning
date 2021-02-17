using System;
using System.Collections.Generic;
using System.Text;
using TaskMicroservice.DataPresentation.Models;

namespace TaskMicroservice.Services.Interfaces
{
     public interface ITaskService
     {
          List<WorkTask> GetTasks();
     }
}
