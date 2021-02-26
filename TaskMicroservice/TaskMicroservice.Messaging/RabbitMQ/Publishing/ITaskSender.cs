using Common.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using TaskMicroservice.DataPresentation.Models;

namespace TaskMicroservice.Messaging.RabbitMQ.Publishing
{
     public interface ITaskSender
     {
          void SendMessage(TaskAssignedMessage task);
     }
}
