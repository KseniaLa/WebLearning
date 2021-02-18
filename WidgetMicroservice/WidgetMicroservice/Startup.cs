using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WidgetMicroservice.Messaging.Configuration;
using WidgetMicroservice.Messaging.Consuming;

namespace WidgetMicroservice
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
               services.AddOptions();

               var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
               var serviceClientSettings = serviceClientSettingsConfig.Get<RabbitMqConfiguration>();
               services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

               if (serviceClientSettings.Enabled)
               {
                    services.AddHostedService<TaskAssignedReceiver>();
               }

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
