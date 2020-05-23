using System;
using System.Collections.Generic;
using System.Text;

namespace Thermo_Server_Domain.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserKey { get; set; }

        public List<Sensor> Sensors { get; set; }
    }
}
