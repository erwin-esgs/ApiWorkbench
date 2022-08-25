using FluentValidation;
using MediatR;
using ApiWorkbench.Properties;
using ApiWorkbench.CQRS.Command.Employee;
using ApiWorkbench.CQRS.Service;
namespace ApiWorkbench.CQRS.Query.Employee.Show
{


    public class EmployeeShowHandler : IRequestHandler<EmployeeShowQuery, EmployeeListResponse>
    {
        //appdbcontext here
        //private readonly AppDbContext _dbContext;
        private readonly IEnumerable<IValidator<EmployeeShowQuery>> _validators;
        private readonly IEmployeeService _employeeService;

        public EmployeeShowHandler(
            //AppDbContext dbContext,
            IEnumerable<IValidator<EmployeeShowQuery>> validators,
            IEmployeeService employeeService
            )
        {
            //_dbContext = dbContext;
            _validators = validators;
            _employeeService = employeeService;
        }
        public async Task<EmployeeListResponse> Handle(EmployeeShowQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler");
            EmployeeListResponse response = new();

            var validator = _validators.Where(x => x.ToString().Contains("EmployeeShowQueryValidator")).First();
            var validate = validator.Validate(request);
            Console.WriteLine(validator);
            Console.WriteLine(validate.IsValid);
            if (validate.IsValid) {
                var result = await _employeeService.ShowEmployee();
                if (result != null) {
                    response.Data = result;
                }
            }
            
            return response;
        }
    }


    /*

    public class EmployeeAddHandler : IPipelineBehavior<EmployeeAddRequest, EmployeeAddResponse>
    {
        private readonly AppDbContext _context;
        private IValidator<EmployeeAddRequest> _validator;

        public EmployeeAddHandler(
            AppDbContext context,
            IValidator<EmployeeAddRequest> validator
        ) {
            _context = context;
            _validator = validator;
        }

        public Task<EmployeeAddResponse> Handle(EmployeeAddRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<EmployeeAddResponse> next)
        {
            EmployeeAddResponse response = new();
            var validationResult = _validator.Validate(request);
            Console.WriteLine(validationResult);
            if (validationResult.IsValid)
            {
                response.Status = true;
                Console.WriteLine("OKE bos");
            }
            return next();
        }
    }
      
     
    public class EmployeeAddHandler : IRequestHandler<EmployeeAddRequest, EmployeeAddResponse>
    {
        private readonly AppDbContext _context;
        private IValidator<EmployeeAddRequest> _validator;

        public EmployeeAddHandler(
            AppDbContext context,
            IValidator<EmployeeAddRequest> validator
        )
        {
            _context = context;
            _validator = validator;
        }
        public async Task<EmployeeAddResponse> Handle(EmployeeAddRequest request, CancellationToken cancellationToken)
        {
            EmployeeAddResponse response = new();
            var validationResult = _validator.Validate(request);
            Console.WriteLine(validationResult);
            if (validationResult.IsValid)
            {
                response.Status = true;
                Console.WriteLine("OKE bos");
            }

            return response;
        }
    }
    */
}

