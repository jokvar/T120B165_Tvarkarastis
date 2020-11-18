using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using T120B165.Data;
using T120B165.Models;

namespace T120B165
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                //var user = new IdentityUser("petrasaaa");
                //userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
                //var user = new IdentityUser("studentas");
                //userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
                //user = new IdentityUser("antras22");
                //userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
                SeedData.Initialize(services);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("https://0.0.0.0:5000");
                });
    }
}
