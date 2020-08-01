using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Thermo_Raspberry_Pi.Web
{
    public interface ISender
    {
        public Task AddTemperature(string userKey, IEnumerable<Temperature> temps);
        public Task<int> CheckUser(string user);
        public Task AddUser(string userKey, IEnumerable<string> sensors);

    }
}
