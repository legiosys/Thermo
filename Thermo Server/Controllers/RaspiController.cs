using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Thermo_Server_Domain.DTO;

namespace Thermo_Server.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class RaspiController : ControllerBase
    {
        private readonly ILogger _logger;

        public RaspiController(ILogger<RaspiController> logger)
        {
            _logger = logger;
        }

        /// <summary>Adds temperature to db</summary>
        /// <remarks>Sample request: 
        /// 
        ///     POST {
        ///         "SensorId": "human",
        ///         "Temp" : 36.6
        ///     }
        ///     
        /// </remarks>
        [HttpPost("temp")]
        public async Task<ActionResult<RaspiTemp>> PostTemp(RaspiTemp temp)
        {
            return temp;
        }
    }
}
