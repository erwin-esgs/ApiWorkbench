using FluentValidation;
using MediatR;
using ApiWorkbench.Properties;
using ApiWorkbench.CQRS.Command.Employee;
using ApiWorkbench.CQRS.Service;
namespace ApiWorkbench.CQRS.Query.Employee.Detail
{
    public class EmployeeDetailHandler : IRequestHandler<EmployeeDetailQuery, EmployeeResponse>
    {
        //appdbcontext here
        //private readonly AppDbContext _dbContext;
        private readonly IEnumerable<IValidator<EmployeeDetailQuery>> _validators;
        private readonly IMediator _mediator;
        private readonly IEmployeeService _employeeService;

        public EmployeeDetailHandler(
            //AppDbContext dbContext,
            IEnumerable<IValidator<EmployeeDetailQuery>> validators,
            IMediator mediator,
            IEmployeeService employeeService
            )
        {
            //_dbContext = dbContext;
            _validators = validators;
            _mediator = mediator;
            _employeeService = employeeService;
        }
        public async Task<EmployeeResponse> Handle(EmployeeDetailQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler");
            EmployeeResponse response = new();

            var validator = _validators.Where(x => x.ToString().Contains("EmployeeDetailQueryValidator")).First();
            var validate = validator.Validate(request);
            Console.WriteLine(validator);
            Console.WriteLine(validate.IsValid);
            if (validate.IsValid)
            {
                response.Data = await _employeeService.GetEmployee(request.Id);

                //var result = _dbContext.Employees.FirstOrDefault(x => x.Id == request.Id);
                //if (result != null)
                //{
                //    response.Data = result;
                //}
            }


            return response;
        }
    }
    /*
    public class EmployeeDetailBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public EmployeeDetailBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //PRE
            Console.WriteLine("Pipeline");
            return next();
            //POST
        }
    }
    */
}

