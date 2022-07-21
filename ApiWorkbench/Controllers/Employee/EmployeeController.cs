using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MediatR;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text.Json;
using ApiWorkbench.CQRS.Command.Employee.Create;
using ApiWorkbench.CQRS.Command.Employee.Edit;
using ApiWorkbench.CQRS.Command.Employee.Delete;
using ApiWorkbench.CQRS.Query.Employee.Detail;
using ApiWorkbench.CQRS.Query.Employee.Show;
using ApiWorkbench.CQRS.Command.Employee;


using FluentValidation;
namespace ApiWorkbench.Controllers.Employee
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly IMediator _mediator;
        public EmployeeController(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> show()
        {
            Console.WriteLine("SHOW");
            EmployeeShowQuery employeeShowCommand = new();
            //return Ok();
            return Ok(await _mediator.Send(employeeShowCommand));
        }

        [HttpGet]
        [Route("{_id}")]
        public async Task<IActionResult> detail(int _id)
        {
            Console.WriteLine("DETAIL");
            Console.WriteLine(_id);
            return Ok(await _mediator.Send(new EmployeeDetailQuery(_id)));
        }

        [HttpPost]
        public async Task<IActionResult> add([FromBody] EmployeeAddCommand request)
        {
            Console.WriteLine("ADD");
            return Ok(await _mediator.Send(request));
            //Console.WriteLine(JsonConvert.SerializeObject(request));
            //return Ok();
        }

        [HttpPatch]
        [Route("{_id}")]
        public async Task<IActionResult> edit(int _id ,
            //[FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)]  EmployeeEditCommand? request=default
            //[FromBody] string request,
            [FromBody] object request
            )
        {
            dynamic data = JsonConvert.DeserializeObject<dynamic>(request.ToString());
            //Console.WriteLine(data);

            Console.WriteLine("EDIT");
            EmployeeEditCommand employeeEditCommand = new();
            employeeEditCommand.Id = _id;
            employeeEditCommand.FirstName = (string)data["FirstName"];
            employeeEditCommand.LastName = (string)data["LastName"];

            Console.WriteLine(JsonConvert.SerializeObject(employeeEditCommand) );

            return Ok(await _mediator.Send(employeeEditCommand));
            //return Ok();
        }

        [HttpDelete]
        [Route("{_id}")]
        public async Task<IActionResult> delete(int _id)
        {
            Console.WriteLine("DELETE");
            EmployeeDeleteCommand request = new (_id);
            return Ok(await _mediator.Send(request));
        }
    }
}

