using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static BKW.Logger.Logger;

namespace L03Order
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            Log("1: Enter Main");
            Log("2: Invoke CreateHostBuilder");
            var one = CreateHostBuilder(args);
            Log("3: Returned from CreateHostBuilder");
            //initialize the host
            Log("4: Invoking Build");
            var two = one.Build();
            Log("5: Back from Build");
            //run application
            Log("6: Invoking Run");
            two.Run();
            Log("7: Back from Run");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) //=>
            // Host.CreateDefaultBuilder(args)
            //     .ConfigureWebHostDefaults(webBuilder =>
            //     {
            //         webBuilder.UseStartup<Startup>();
            //     });
            {
                Log("1: Entering CreateHostBuilder");
                //initialize new instance of host builder
                Log("2: Invoking Host.CreateDefaultBuilder");
                IHostBuilder one = Host.CreateDefaultBuilder(args);
                Log("3: Back from CreateDefaultBuilder");
                Log("4: Creating the Action<IHostBuilder> variable d");
                Action<IWebHostBuilder> d = webBuilder =>
                {
                    Log("1: Entering webBuilder Lambda");

                    Log("2: Invoking UseStartup<StartUP>");
                    //specify the startup type
                    webBuilder.UseStartup<Startup>();
                    Log("3: Back from UseStartup<StartUP>");
                    Log("4: Exiting webBuilder Lambda");
                };
                Log("5: d had been assigned");

                Log("6: Invoking ConfigureWebHostDefaults");
                IHostBuilder two = one.ConfigureWebHostDefaults(d);
                Log("7: Back from invoking ConfigureWebHostDefaults");

                Log("8: Returning from CretaeHostBuilder");
                return two;
            }
    }
}
