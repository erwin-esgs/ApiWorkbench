using MediatR;
using ApiWorkbench.Models;
using FluentValidation;
using ApiWorkbench.Properties;
using ApiWorkbench.CQRS.Service;

namespace ApiWorkbench.CQRS.Command.Employee.Create
{
    public class EmployeeAddHandler : IRequestHandler<EmployeeAddCommand, EmployeeListResponse>
    {
        //appdbcontext here
        //private readonly AppDbContext _dbContext;
        private readonly IEnumerable<IValidator<EmployeeAddCommand>> _validators;
        private readonly IEmployeeService _employeeService;

        public EmployeeAddHandler(
            //AppDbContext dbContext,
            IEnumerable<IValidator<EmployeeAddCommand>> validators,
            IEmployeeService employeeService
            )
        {
            //_dbContext = dbContext;
            _validators = validators;
            _employeeService = employeeService;
        }
        public async Task<EmployeeListResponse> Handle(EmployeeAddCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler");
            EmployeeListResponse response = new();

            var validator = _validators.Where(x => x.ToString().Contains("EmployeeAddCommandValidator")).First();
            var validate = validator.Validate(request);

            //Console.WriteLine(result.IsValid);
            if (validate.IsValid)
            {

                var result = await _employeeService.AddEmployee(request);
                if (result == true)
                {
                    response.Data = await _employeeService.ShowEmployee();
                }
                //EmployeeModel model = new();
                //model.FirstName = request.FirstName;
                //model.LastName = request.LastName;

                //Console.WriteLine(_dbContext.Employees.Add(model));
                //Console.WriteLine(_dbContext.SaveChanges());

                //response.Data = _dbContext.Employees.ToList();
            }


            return response;
        }
    }
}

