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
using Thermo_Server_Raspberry.Temperature;

namespace Thermo_Server_WebApi.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class RaspiController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly DomainContext _context;
        private readonly UserConfigurating _userConfigurating;

        public RaspiController(ILogger<RaspiController> logger, DomainContext context, UserConfigurating userConfigurating)
        {
            _logger = logger;
            _context = context;
            _userConfigurating = userConfigurating;
        }

        /// <summary>Adds temperature to db</summary>
        /// <remarks>Sample request: 
        /// 
        ///     POST {
        ///         "UserId": 1,
        ///         "HardwareId": "human",
        ///         "Temp" : 36.6
        ///     }
        ///     
        /// </remarks>
        [HttpPost("Temp")]
        public async Task<ActionResult<RaspiTemp>> PostTemp(RaspiTemp temp)
        {
            return temp;
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
