using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ApiWorkbench.CQRS.Command.BloodPressure.Get;
namespace ApiWorkbench.Controllers.BloodPressure
{

    [ApiController]
    [Route("[controller]")]
    public class BloodPressureController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BloodPressureController(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> add([FromBody] BloodPressureCommand request)
        {
            Console.WriteLine("CALCULATE");
            return Ok(await _mediator.Send(request));
        }
    }
}

