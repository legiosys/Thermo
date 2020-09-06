using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IConfiguration _configuration;
        private bool _work = false;
        private string UserKey = "TestUser";
        public MainService(ISensor sensor, ISender sender, ILogger<MainService> logger, IConfiguration configuration)
        {
            _sensor = sensor;
            _sender = sender;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Run()
        {
            await CheckEnvironment();
            var frequency = _configuration.GetValue<int>("SendFrequency");
            while(_work)
            {
                var temps = _sensor.GetTemperatures();
                await _sender.AddTemperature(UserKey, temps);
                await Task.Delay(frequency * 60 * 1000);
            }
        }

        public async Task CheckEnvironment()
        {
            if (CheckSensorsState() &&
                await CheckUserExistance())
                _work = true;
            await Task.Delay(1000);
        }

        private bool CheckSensorsState()
        {
            try
            {
                UserKey = _sensor.GetSerial();
                var sensors = _sensor.GetSensors();
                if (sensors == null || sensors.Count() == 0)
                    throw new Exception("Sensors not exist!");
                if (sensors.Any(x => string.IsNullOrWhiteSpace(x)))
                    throw new Exception("Bad sensors exist!");
                if (_sensor.GetTemperatures().Any(x => float.IsNaN(x.Value)))
                    throw new Exception("Bad sensor temp");
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message + "\n" + e.StackTrace);
                return false;
            }
            _logger.LogInformation("Sensors passed check.");
            return true;
           
        }
        
        private async Task<bool> CheckUserExistance()
        {
            _logger.LogInformation("Checking connection to server");
            try
            {
                if (await _sender.CheckUser(UserKey) < 0)
                {
                    await _sender.AddUser(UserKey, _sensor.GetSensors());
                    _logger.LogInformation("User created, starting...");
                }
                else
                    _logger.LogInformation("User exists, starting...");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + "\n" + e.StackTrace);
                return false;
            }
            return true;

        }
    }
}
