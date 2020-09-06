using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Thermo_Server_Domain.Model
{
    public class Temperature
    {
        [Key]
        public int Id { get; set; }
        public float Value { get; set; }
        public DateTime Time { get; set; }
        public int SensorId { get; set; }
        public Sensor Sensor { get; set; }
    }
}
