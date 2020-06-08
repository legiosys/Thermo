using System;
using System.Collections.Generic;
using System.Text;

namespace Thermo_Server_Domain.DTO
{
    public class DtoUser
    {
        public string UserKey { get; set; }
        public IEnumerable<string> Sensors { get; set; }
    }
}
