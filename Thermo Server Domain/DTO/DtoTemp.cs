using System;
using System.Collections.Generic;
using System.Text;

namespace Thermo_Server_Domain.DTO
{
    public class DtoTemp
    {
        public string Name { get; set; }
        public float Value { get; set; }
        public DateTime Time { get; set; }
    }
}
