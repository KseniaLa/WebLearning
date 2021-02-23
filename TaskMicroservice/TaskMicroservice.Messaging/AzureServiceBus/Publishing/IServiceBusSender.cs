using Common.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskMicroservice.Messaging.AzureServiceBus.Publishing
{
     public interface IServiceBusSender
     {
          Task SendMessage(TaskAssignedMessage payload);
     }
}
