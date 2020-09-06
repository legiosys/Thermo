using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Thermo_Server_Domain.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserKey { get; set; }

        public List<Sensor> Sensors { get; set; }
    }
}
