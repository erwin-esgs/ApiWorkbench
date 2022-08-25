using FluentValidation;
using MediatR;
using System.Collections.Generic;
using ApiWorkbench.Models;
using ApiWorkbench.Properties;
using ApiWorkbench.CQRS.Service;

namespace ApiWorkbench.CQRS.Command.Employee.Delete
{

    public class EmployeeEditHandler : IRequestHandler<EmployeeDeleteCommand, EmployeeListResponse>
    {
        //appdbcontext here
        //private readonly AppDbContext _dbContext;
        private readonly IEnumerable<IValidator<EmployeeDeleteCommand>> _validators;
        //private readonly IMediator _mediator;
        private readonly IEmployeeService _employeeService;

        public EmployeeEditHandler(
            //AppDbContext dbContext,
            IEnumerable<IValidator<EmployeeDeleteCommand>> validators,
            //IMediator mediator,
            IEmployeeService employeeService
            )
        {
            //_dbContext = dbContext;
            _validators = validators;
            //_mediator = mediator;
            _employeeService = employeeService;
        }
        public async Task<EmployeeListResponse> Handle(EmployeeDeleteCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler");
            EmployeeListResponse response = new();

            var validator = _validators.Where(x => x.ToString().Contains("EmployeeDeleteCommandValidator")).First();
            var validate = validator.Validate(request);

            Console.WriteLine(validator);
            Console.WriteLine(validate.IsValid);

            if (validate.IsValid)
            {
                //var result = _dbContext.Employees.FirstOrDefault(x => x.Id == request.Id);
                var result = await _employeeService.DelEmployee(request.Id);
                if (result == true) {
                    response.Data = await _employeeService.ShowEmployee();
                }
                //if (result != null)
                //{
                //    _dbContext.Employees.Attach(result);
                //    _dbContext.Employees.Remove(result);

                //    Console.WriteLine(_dbContext.SaveChanges());
                //    response.Data = _dbContext.Employees.ToList();
                //}
            } 


            return response;
        }
    }
}

