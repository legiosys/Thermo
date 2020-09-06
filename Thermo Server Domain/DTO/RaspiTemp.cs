using System;
using System.Collections.Generic;
using System.Text;

namespace Thermo_Server_Domain.DTO
{
    public class RaspiTemp
    {
        public string UserKey { get; set; }
        public IEnumerable<RaspiSensor> Sensors { get; set; }
    }
    public class RaspiSensor
    {
        public string HardwareId { get; set; }
        public float Value { get; set; }
    }
}
