using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace Thermo_Raspberry_Pi.Web
{
    public class Sender : ISender
    {
        private readonly string BaseUrl;
        public Sender(IConfiguration configuration)
        {
            BaseUrl = configuration.GetValue<string>("Url");
        }
        public async Task AddTemperature(string userKey, IEnumerable<Temperature> temperatures)
        {
            var url = string.Concat(BaseUrl, "Temp");
            await url.PostJsonAsync(new { UserKey = userKey, Sensors = temperatures });           
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
