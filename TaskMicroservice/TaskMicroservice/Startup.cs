using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskMicroservice.Messaging.AzureServiceBus.Configuration;
using TaskMicroservice.Messaging.AzureServiceBus.Publishing;
using TaskMicroservice.Messaging.RabbitMQ.Configuration;
using TaskMicroservice.Messaging.RabbitMQ.Publishing;
using TaskMicroservice.Services;
using TaskMicroservice.Services.Interfaces;

namespace TaskMicroservice
{
     public class Startup
     {
          public Startup(IConfiguration configuration)
          {
               Configuration = configuration;
          }

          public IConfiguration Configuration { get; }

          // This method gets called by the runtime. Use this method to add services to the container.
          public void ConfigureServices(IServiceCollection services)
          {
               services.Scan(scan => scan
                 .FromAssembliesOf(new List<Type> { typeof(ITaskService), typeof(TaskService) })
                 .AddClasses(classes => classes.AssignableTo<IScopedService>())
                 .AsImplementedInterfaces()
                 .WithScopedLifetime()
               );

               services.AddOptions();

               services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));
               services.AddSingleton<ITaskSender, TaskSender>();

               services.Configure<AzureServiceBusConfiguration>(Configuration.GetSection("AzureServiceBus"));
               services.AddSingleton<IServiceBusSender, ServiceBusSender>();

               services.AddControllers();
          }

          // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
          public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
          {
               if (env.IsDevelopment())
               {
                    app.UseDeveloperExceptionPage();
               }

               app.UseRouting();

               app.UseAuthorization();

               app.UseEndpoints(endpoints =>
               {
                    endpoints.MapControllers();
               });
          }
     }
}
