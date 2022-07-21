using MediatR;
using FluentValidation;
using ApiWorkbench.CQRS.Command.Employee;

namespace ApiWorkbench.CQRS.Query.Employee.Show
{
    public class EmployeeShowQuery : IRequest<EmployeeListResponse>
    {
    }

    public class EmployeeShowQueryValidator : AbstractValidator<EmployeeShowQuery>
    {
        public EmployeeShowQueryValidator()
        {

        }
    }

}

