using Common.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserMicroservice.Messaging.AzureServiceBus.Consuming
{
     public interface ITaskProcessor
     {
          void Process(TaskAssignedMessage myPayload);
     }
}
