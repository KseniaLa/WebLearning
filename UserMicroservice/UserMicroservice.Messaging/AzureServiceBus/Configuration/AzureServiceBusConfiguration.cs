using System;
using System.Collections.Generic;
using System.Text;

namespace UserMicroservice.Messaging.AzureServiceBus.Configuration
{
     public class AzureServiceBusConfiguration
     {
          public string ConnectionString { get; set; }

          public string QueueName { get; set; }
     }
}
