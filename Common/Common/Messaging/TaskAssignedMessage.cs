using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging
{
     public class TaskAssignedMessage
     {
          public int TaskId { get; set; }
          public string TaskName { get; set; }
          public int AssignedByUserId {get; set;}
          public int AssignedToUserId { get; set; }
     }
}
