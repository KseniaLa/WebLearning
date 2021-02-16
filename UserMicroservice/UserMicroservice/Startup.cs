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
using UserMicroservice.Services;
using UserMicroservice.Services.Interfaces;

namespace UserMicroservice
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
                 .FromAssembliesOf(new List<Type> { typeof(IUserService), typeof(UserService) })
                 .AddClasses(classes => classes.AssignableTo<IScopedService>())
                 .AsImplementedInterfaces()
                 .WithScopedLifetime()
               );

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
