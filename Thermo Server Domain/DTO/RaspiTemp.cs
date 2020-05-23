using System;
using System.Collections.Generic;
using System.Text;

namespace Thermo_Server_Domain.DTO
{
    public class RaspiTemp
    {
        public string SensorId { get; set; }
        public float Temp { get; set; }
    }
}
