using System;
using System.Collections.Generic;
using System.Text;

namespace Thermo_Server_Domain.Model
{
    public class Temperature
    {
        public int Id { get; set; }
        public float Value { get; set; }
        public DateTime Time { get; set; }
        public int SensorId { get; set; }
        public Sensor Sensor { get; set; }
    }
}
