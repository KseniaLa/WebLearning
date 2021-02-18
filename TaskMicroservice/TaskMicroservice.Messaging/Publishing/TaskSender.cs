using Common.Messaging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using TaskMicroservice.DataPresentation.Models;
using TaskMicroservice.Messaging.Configuration;

namespace TaskMicroservice.Messaging.Publishing
{
     public class TaskSender : ITaskSender
     {
          private readonly string _hostname;
          private readonly string _password;
          private readonly string _queueName;
          private readonly string _username;
          private IConnection _connection;

          public TaskSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
          {
               _queueName = rabbitMqOptions.Value.QueueName;
               _hostname = rabbitMqOptions.Value.Hostname;
               _username = rabbitMqOptions.Value.UserName;
               _password = rabbitMqOptions.Value.Password;

               CreateConnection();
          }

          public void SendMessage(TaskAssignedMessage task)
          {
               if (ConnectionExists())
               {
                    using (var channel = _connection.CreateModel())
                    {
                         //channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                         channel.ExchangeDeclare(exchange: "tasks", type: ExchangeType.Fanout);

                         var json = JsonConvert.SerializeObject(task);
                         var body = Encoding.UTF8.GetBytes(json);

                         //channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
                         channel.BasicPublish(exchange: "tasks", routingKey: "", basicProperties: null, body: body);
                    }
               }
          }

          private void CreateConnection()
          {
               try
               {
                    var factory = new ConnectionFactory
                    {
                         HostName = _hostname,
                         UserName = _username,
                         Password = _password
                    };
                    _connection = factory.CreateConnection();
               }
               catch (Exception ex)
               {
                    Console.WriteLine($"Could not create connection: {ex.Message}");
               }
          }

          private bool ConnectionExists()
          {
               if (_connection != null)
               {
                    return true;
               }

               CreateConnection();

               return _connection != null;
          }
     }
}
