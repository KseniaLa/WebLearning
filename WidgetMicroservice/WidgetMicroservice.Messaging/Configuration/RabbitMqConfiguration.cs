﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WidgetMicroservice.Messaging.Configuration
{
     public class RabbitMqConfiguration
     {
          public string Hostname { get; set; }

          public string QueueName { get; set; }

          public string UserName { get; set; }

          public string Password { get; set; }

          public bool Enabled { get; set; }
     }
}
