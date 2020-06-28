using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Thermo_Raspberry_Pi.Hardware;
using Thermo_Raspberry_Pi.Web;

namespace Thermo_Raspberry_Pi
{
    class Program
    {
        public static IConfiguration configuration;
        static async Task Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            var temp = new MockSensor();
            var web = new Sender();
            var url = configuration.GetSection("Url");
            Console.WriteLine(url.Value);
            //await web.AddUser("TestUser", temp.GetSensors());
            //await web.AddTemperature("TestUser", temp.GetTemperatures());
            //Check(temp);
        }
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {            

            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);

        }
    }
}
