using System;
using System.Collections.Generic;
using System.Text;

namespace Thermo_Server_Domain.Model
{
    public class Temperature
    {
        public int Id { get; set; }
        public float Temp { get; set; }
        public DateTime Time { get; set; }
        public int SensorId { get; set; }
        
    }
}
