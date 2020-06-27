using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;

namespace Thermo_Raspberry_Pi.Web
{
    public class Sender : IConnector
    {
        public string BaseUrl = "http://localhost:8000/Raspi/";
        public async Task AddTemperature(string userKey, IEnumerable<Temperature> temperatures)
        {
            var url = string.Concat(BaseUrl, "Temp");
            foreach(var temp in temperatures)
            {
                await url.PostJsonAsync(new { UserKey = userKey, temp.HardwareId, temp.Value });
            }
        }

        public async Task<int> CheckUser(string user)
        {
            var url = string.Concat(BaseUrl, "User/", user);
            return await url.GetJsonAsync<int>();
        }

        public async Task AddUser(string userKey, IEnumerable<string> sensors)
        {
            var url = string.Concat(BaseUrl, "User");
            await url.PostJsonAsync(new { UserKey = userKey, Sensors = sensors });
        }
    }
}
