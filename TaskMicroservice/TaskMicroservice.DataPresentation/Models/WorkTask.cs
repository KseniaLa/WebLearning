using System;
using System.Collections.Generic;
using System.Text;

namespace TaskMicroservice.DataPresentation.Models
{
     public class WorkTask
     {
          public int Id { get; set; }
          public string Title { get; set; }
          public int AssignedByUserId { get; set; }
          public int AssignedToUserId { get; set; }
     }
}
