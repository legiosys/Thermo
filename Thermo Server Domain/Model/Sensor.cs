using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Thermo_Server_Domain.Model
{
    public class Sensor
    {
        [Key]
        public int SensorId { get; set; }
        public string HardwareId { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Temperature> Temperatures { get; set; }
    }
}
