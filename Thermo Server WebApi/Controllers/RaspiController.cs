using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Thermo_Server_Domain.DTO;
using Thermo_Server_Domain.Model;
using Thermo_Server_Raspberry;

namespace Thermo_Server_WebApi.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class RaspiController : ControllerBase
    {
        private readonly UserConfigurating _userConfigurating;
        private readonly TemperatureService _temperatureService;

        public RaspiController(UserConfigurating userConfigurating, TemperatureService temperatureService)
        {
            _userConfigurating = userConfigurating;
            _temperatureService = temperatureService;
        }

        /// <summary>Adds temperature to db</summary>
        /// <remarks>Sample request: 
        /// 
        ///     POST [{
        ///         "UserKey": "TestUser",
        ///         "HardwareId": "human",
        ///         "Value" : 36.6
        ///     }]
        ///     
        /// </remarks>
        [HttpPost("Temp")]
        public async Task<ActionResult> PostTemp(IEnumerable<RaspiTemp> temps)
        {
            await _temperatureService.AddTemperature(temps);
            return Ok();
        }

        /// <summary>Adds user to db, returns user id</summary>
        /// <remarks>Sample request: 
        /// 
        ///     POST {
        ///         "UserKey": "TestUser",
        ///         "Sensors": ["human", "dog"]
        ///     }
        ///     
        /// </remarks>
        [HttpPost("User")]
        public async Task<ActionResult<int>> AddUser(DtoUser dtouser)
        {
            var id = await _userConfigurating.AddUser(dtouser);
            return Ok(id);
        }

        /// <summary>Checks existance user on server and gets user id</summary>
        /// <remarks>Sample request: 
        /// 
        ///     GET /Raspi/User/TestUser
        ///     
        /// </remarks>
        [HttpGet("User/{key}")]
        public async Task<ActionResult<int>> GetUser(string key)
        {
            var id = await _userConfigurating.GetUser(key);
            return Ok(id);
        }
    }
}
