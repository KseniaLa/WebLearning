using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserMicroservice.Messaging.AzureServiceBus.Consuming
{
     public interface IServiceBusConsumer
     {
          void RegisterOnMessageHandlerAndReceiveMessages();
          Task CloseQueueAsync();
     }
}
