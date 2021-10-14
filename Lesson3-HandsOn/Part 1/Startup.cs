using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lesson3_HandsOn
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //c is an HTTPContext and n is a Func<Task>
            app.Use(async (c,n) =>{
                //Print to console
                await Console.Out.WriteAsync(c.Request.Method + '\n');
                await Console.Out.WriteAsync(c.Request.Path + '\n');
                //Check the path to see whether we want to short circuit
                if(c.Request.Path == "/short"){
                    await c.Response.WriteAsync(c.Request.Path + '\n');
                    await c.Response.WriteAsync("Short circuiting" + '\n');
                    return;
                } 
                //Print to the page
                await c.Response.WriteAsync(c.Request.Path + '\n');
                await c.Response.WriteAsync("Not short circuiting" + '\n');
                //Without n(), everything below will be short circuited
                await n();
            });

            //Run is terminal so the rest of configure will be short circuited
            app.Run(async (c) => {
                await c.Response.WriteAsync("In app.Run" + '\n');
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
