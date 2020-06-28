using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thermo_Raspberry_Pi.Hardware;
using Thermo_Raspberry_Pi.Web;

namespace Thermo_Raspberry_Pi
{
    public class MainService 
    {
        private readonly ISensor _sensor;
        private readonly ISender _sender;
        private readonly ILogger _logger;
        private bool _work = true;
        private readonly string UserKey = "TestUser";
        public MainService(ISensor sensor, ISender sender, ILogger<MainService> logger )
        {
            _sensor = sensor;
            _sender = sender;
            _logger = logger;
        }

        public void Run()
        {            
            while(_work)
            {
                var temps = _sensor.GetTemperatures();
                _sender.AddTemperature(UserKey, temps);
                _logger.LogInformation("Temperature sended!");
                Thread.Sleep(1 * 60 * 1000);
            }
        }
        
        public async Task CheckUserExistance()
        {
            if (await _sender.CheckUser(UserKey) < 0)
            {
                await _sender.AddUser(UserKey, _sensor.GetSensors());
                _logger.LogInformation("User created, starting...");
            }
            else
                _logger.LogInformation("User exists, starting...");

        }
    }
}
