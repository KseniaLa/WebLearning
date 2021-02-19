using Common.Messaging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserMicroservice.Messaging.Configuration;

namespace UserMicroservice.Messaging.Consuming
{
     public class TaskAssignedReceiver : BackgroundService
     {
          private IModel _channel;
          private IConnection _connection;
          private ILogger _logger;
          private readonly string _hostname;
          private readonly string _exchange;
          private readonly string _username;
          private readonly string _password;

          public TaskAssignedReceiver(IOptions<RabbitMqConfiguration> rabbitMqOptions, ILogger<TaskAssignedReceiver> logger)
          {
               _hostname = rabbitMqOptions.Value.Hostname;
               _exchange = rabbitMqOptions.Value.Exchange;
               _username = rabbitMqOptions.Value.UserName;
               _password = rabbitMqOptions.Value.Password;
               _logger = logger;
               InitializeRabbitMqListener();
          }

          private void InitializeRabbitMqListener()
          {
               var factory = new ConnectionFactory
               {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
               };

               _connection = factory.CreateConnection();
               _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
               _channel = _connection.CreateModel();
          }

          protected override Task ExecuteAsync(CancellationToken stoppingToken)
          {
               stoppingToken.ThrowIfCancellationRequested();

               _channel.ExchangeDeclare(exchange: _exchange, type: ExchangeType.Fanout);

               var queueName = _channel.QueueDeclare().QueueName;
               _channel.QueueBind(queue: queueName, exchange: _exchange, routingKey: "");

               var consumer = new EventingBasicConsumer(_channel);
               consumer.Received += (ch, ea) =>
               {
                    var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var updateCustomerFullNameModel = JsonConvert.DeserializeObject<TaskAssignedMessage>(content);

                    HandleMessage(updateCustomerFullNameModel);
               };
               consumer.Shutdown += OnConsumerShutdown;
               consumer.Registered += OnConsumerRegistered;
               consumer.Unregistered += OnConsumerUnregistered;
               consumer.ConsumerCancelled += OnConsumerCancelled;

               _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

               return Task.CompletedTask;
          }

          private void HandleMessage(TaskAssignedMessage taskAssignedMessage)
          {
               _logger.LogInformation($"Received message: TaskId: {taskAssignedMessage.TaskId}, TaskName: {taskAssignedMessage.TaskName}, Users: {taskAssignedMessage.AssignedByUserId}-{taskAssignedMessage.AssignedToUserId}");
          }

          private void OnConsumerCancelled(object sender, ConsumerEventArgs e)
          {
          }

          private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
          {
          }

          private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
          {
          }

          private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
          {
          }

          private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
          {
          }

          public override void Dispose()
          {
               _channel.Close();
               _connection.Close();
               base.Dispose();
          }
     }
}
