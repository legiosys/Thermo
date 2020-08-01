using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Thermo_Raspberry_Pi.Services
{
    public class FileSystemService
    {
        public string GetSerial()
        {
            var cpuinfo = File.ReadAllText("/proc/cpuinfo");
            var match = (new Regex(@"Serial\s*:\s([0-9A-z]*)")).Match(cpuinfo);
            return match.Groups[1].Value;
        }

        public IEnumerable<string> GetSensors()
        {
            return File.ReadAllLines("/sys/bus/w1/devices/w1_bus_master1/w1_master_slaves");
        }

        public float GetSensorTemp(string id)
        {
            var tString = File.ReadAllText($"/sys/bus/w1/devices/{id}/w1_slave");
            var match = (new Regex(@"t=([0-9]*)$")).Match(tString);
            return ParseTemp(match.Groups[1].Value);
        }

        private float ParseTemp(string temp)
        {
            temp = temp.Insert(temp.Length - 3, ".");
            return (float)Math.Round(float.Parse(temp), 1);
        }       
    }
}
