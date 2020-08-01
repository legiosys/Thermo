using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Thermo_Raspberry_Pi.Web
{
    public class Sender : ISender
    {
        private readonly string BaseUrl;
        private readonly ILogger _logger;
        public Sender(IConfiguration configuration, ILogger<Sender> logger)
        {
            BaseUrl = configuration.GetValue<string>("Url");
            _logger = logger;
        }
        public async Task AddTemperature(string userKey, IEnumerable<Temperature> temperatures)
        {
            var url = string.Concat(BaseUrl, "Temp");
            try
            {
                await url.PostJsonAsync(new { UserKey = userKey, Sensors = temperatures });
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
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
            try
            {
                await url.PostJsonAsync(new { UserKey = userKey, Sensors = sensors });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
