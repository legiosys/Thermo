using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Thermo_Raspberry_Pi.Hardware;
using Thermo_Raspberry_Pi.Services;
using Thermo_Raspberry_Pi.Web;

namespace Thermo_Raspberry_Pi
{
    class Program
    {
        public static IConfiguration configuration;
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            var mainService = services.BuildServiceProvider().GetService<MainService>();
            mainService.Run().Wait();
        }
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {            

            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddLogging(l => l.AddConsole());
            serviceCollection.AddScoped<FileSystemService>();
            serviceCollection.AddTransient<ISensor, Sensor>();
            serviceCollection.AddTransient<ISender, Sender>();
            serviceCollection.AddSingleton<MainService>();
        }
    }
}
