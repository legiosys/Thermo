using System;
using System.Collections.Generic;
using System.Text;
using Thermo_Raspberry_Pi.Services;

namespace Thermo_Raspberry_Pi.Hardware
{
    public class Sensor : ISensor
    {
        FileSystemService _fs;
        public Sensor(FileSystemService fs)
        {
            _fs = fs;
        }
        public IEnumerable<string> GetSensors()
        {
            return _fs.GetSensors();
        }

        public IEnumerable<Temperature> GetTemperatures()
        {
            var sensors = GetSensors();
            foreach(var sensor in sensors)
            {
                var temp = _fs.GetSensorTemp(sensor);
                yield return new Temperature()
                {
                    HardwareId = sensor,
                    Value = temp
                };
            }
        }

        public string GetSerial()
        {
            return _fs.GetSerial();
        }
    }
}
