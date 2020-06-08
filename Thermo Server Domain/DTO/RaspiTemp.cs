using System;
using System.Collections.Generic;
using System.Text;

namespace Thermo_Server_Domain.DTO
{
    public class RaspiTemp
    {
        public string UserKey { get; set; }
        public string HardwareId { get; set; }
        public float Value { get; set; }
    }
}
