using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thermo_Server_Domain.DTO;
using Thermo_Server_Raspberry;

namespace Thermo_Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly TemperatureService _temperatureService;
       public ClientController(TemperatureService temperatureService) 
        {
            _temperatureService = temperatureService;
        }
        /// <summary>Gets all temperatures for user ID</summary>
        /// <remarks>Sample request: 
        /// 
        ///     GET /Raspi/User/TestUser
        ///     
        /// </remarks>
        [HttpGet("{key}")]
        public async Task<IEnumerable<DtoTemp>> Get(string key)
        {
            var list = await _temperatureService.GetTemperatures(key);
            return list;
        }

        
    }
}
