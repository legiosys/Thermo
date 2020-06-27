using System;
using Microsoft.Extensions.Logging;
using Thermo_Server_Domain.Model;
using Thermo_Server_Domain.DTO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Thermo_Server_Raspberry
{
    public class UserConfigurating
    {
        private readonly DomainContext _context;
        private readonly ILogger _logger;
        public UserConfigurating(DomainContext context, ILogger<UserConfigurating> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> GetUser(string key)
        {
            var user = await _context.Users.FirstOrDefaultAsync(
                u => u.UserKey.Equals(key));
            if (user != null)
                return user.UserId;
            else
            {
                _logger.LogInformation($"GetUser:User '{key}' doesn't exist.");
                return -1;
            }
        }
        public async Task<int> AddUser(DtoUser dtoUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(
                u => u.UserKey.Equals(dtoUser.UserKey));
            var sensors = SensorsList(dtoUser.Sensors);
            if(user == null)
            {
                user = new User();
                user.UserKey = dtoUser.UserKey;
                _context.Users.Add(user);
                _logger.LogInformation($"AddUser:User '{dtoUser.UserKey}' created.");
            }
            user.Sensors = sensors;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"AddUser:User '{dtoUser.UserKey}' installed sensors: {string.Join(',', dtoUser.Sensors)}.");
            return user.UserId;
        }

        private List<Sensor> SensorsList(IEnumerable<string> dtoSensors)
        {
            List<Sensor> sensors = new List<Sensor>();
            foreach (var dtoSensor in dtoSensors)
            {
                var sensor = new Sensor();
                sensor.HardwareId = dtoSensor;
                sensors.Add(sensor);
            }
            return sensors;
        }
    }
}
