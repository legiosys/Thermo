using System;
using Microsoft.Extensions.Logging;
using Thermo_Server_Domain.Model;
using Thermo_Server_Domain.DTO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Thermo_Server_Raspberry
{
    public class TemperatureService
    {
        private readonly DomainContext _context;
        private readonly ILogger _logger;
        public TemperatureService(DomainContext context, ILogger<TemperatureService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddTemperature(RaspiTemp raspiTemp)
        {
            var temp = new Temperature();
            temp.Value = raspiTemp.Value;
            temp.Time = DateTime.Now;
            var sensor = await _context.Sensors.FirstOrDefaultAsync(
                s => s.HardwareId.Equals(raspiTemp.HardwareId) && s.User.UserKey.Equals(raspiTemp.UserKey));
            if (sensor == null)
            {
                _logger.LogError($"AddTemperature:Sensor {raspiTemp.UserKey}/{raspiTemp.HardwareId} doesn't exist!");
                throw new Exception("BAD SENSOR");
            }
            await _context.Entry(sensor).Collection(s => s.Temperatures).LoadAsync();
            sensor.Temperatures.Add(temp);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"AddTemperature:Sensor {raspiTemp.UserKey}/{raspiTemp.HardwareId} added temperature {raspiTemp.Value}.");
        }

        public async Task<List<DtoTemp>> GetTemperatures(string userKey)
        {
            var sensors = await _context.Sensors.Where(s => s.User.UserKey == userKey).ToListAsync();           
            var dtoList = new List<DtoTemp>();
            foreach(var sensor in sensors)
            {
                await _context.Entry(sensor).Collection(s => s.Temperatures).LoadAsync();
                var sensorName= (sensor.Name != null) ? sensor.Name : sensor.HardwareId;
                foreach (var temp in sensor.Temperatures) {
                    var record = new DtoTemp();
                    record.Name = sensorName;
                    record.Time = temp.Time;
                    record.Value = temp.Value;
                    dtoList.Add(record);
                }
            }
            return dtoList;
        }
    }
}
