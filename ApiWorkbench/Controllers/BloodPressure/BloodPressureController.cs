using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using ApiWorkbench.CQRS.Command.BloodPressure.Get;
namespace ApiWorkbench.Controllers.BloodPressure
{

    [ApiController]
    [Route("bp")]
    public class BloodPressureController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BloodPressureController(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> add([FromBody] object request)
        {
            dynamic data = JsonConvert.DeserializeObject<dynamic>(request.ToString());
            Console.WriteLine("CALCULATE");
            return Ok(data);
        }
    }
}

