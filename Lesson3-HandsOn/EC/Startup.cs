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

            app.Map("/One", async (a) => {
                a.Use(async (c,n) => {
                    await c.Response.WriteAsync("In /One" + '\n');
                    if(c.Request.Path == "/short"){
                        await c.Response.WriteAsync("In /short" + '\n');
                        await c.Response.WriteAsync("Short circuiting" + '\n');
                        return;
                    }
                    await c.Response.WriteAsync("Not short circuiting" + '\n');
                    await n();
                });

                a.Run(async (c) => {
                    await c.Response.WriteAsync("In " + c.Request.Path + '\n');
                });
            });

            app.Map("/Two", async (a) => {
                a.Map("/Mapped", async (a) => {
                    a.Run(async (c) => {
                        await c.Response.WriteAsync(
                            "In map /Mapped" + '\n' +
                            "In app.Run" + '\n'
                        );
                    });
                });

                a.Run(async (c) => {
                    await c.Response.WriteAsync("In /Two" + c.Request.Path + '\n');
                });
            });

            app.Run(async (c) => {
                await c.Response.WriteAsync("Not in /One or /Two");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("In /");
                });
            });
        }
    }
}
