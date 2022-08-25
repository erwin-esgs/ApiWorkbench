using FluentValidation;
using MediatR;
using System.Collections.Generic;
using ApiWorkbench.Models;
using ApiWorkbench.Properties;
using ApiWorkbench.CQRS.Service;

namespace ApiWorkbench.CQRS.Command.Employee.Edit
{

    public class EmployeeEditHandler : IRequestHandler<EmployeeEditCommand, EmployeeListResponse>
    {
        //appdbcontext here
        //private readonly AppDbContext _dbContext;
        private readonly IEnumerable<IValidator<EmployeeEditCommand>> _validators;
        private readonly IEmployeeService _employeeService;
        //private readonly IMediator _mediator;

        public EmployeeEditHandler(
            //AppDbContext dbContext,
            IEnumerable<IValidator<EmployeeEditCommand>> validators,
            IEmployeeService employeeService
            //IMediator mediator
            )
        {
            //_dbContext = dbContext;
            _validators = validators;
            //_mediator = mediator;
            _employeeService = employeeService;
        }
        public async Task<EmployeeListResponse> Handle(EmployeeEditCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler");
            EmployeeListResponse response = new();

            var validator = _validators.Where(x => x.ToString().Contains("EmployeeEditCommandValidator")).First();
            var validate = validator.Validate(request);
            Console.WriteLine(validator);
            Console.WriteLine(validate.IsValid);
            if (validate.IsValid)
            {
                var result = await _employeeService.EditEmployee(request);
                if (result)
                {
                    response.Data = await _employeeService.ShowEmployee();
                }
                //var result = _dbContext.Employees.FirstOrDefault(x => x.Id == request.Id);
                //if (result != null)
                //{
                //    if (request.FirstName != null) result.FirstName = request.FirstName;
                //    if (request.LastName != null ) result.LastName = request.LastName;

                //    Console.WriteLine(_dbContext.SaveChanges());
                //    response.Data = _dbContext.Employees.ToList();
                //}
            } 

            return response;
        }
    }
}

