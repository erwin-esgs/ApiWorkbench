
using MediatR;
using FluentValidation;

namespace ApiWorkbench.CQRS.Command.Employee.Create
{
    public class EmployeeAddCommand : IRequest<EmployeeListResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class EmployeeAddCommandValidator : AbstractValidator<EmployeeAddCommand>
    {
        public EmployeeAddCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();

        }
    }

}

