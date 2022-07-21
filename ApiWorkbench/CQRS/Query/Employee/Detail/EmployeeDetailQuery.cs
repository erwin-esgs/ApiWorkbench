using MediatR;
using FluentValidation;
using ApiWorkbench.CQRS.Command.Employee;

namespace ApiWorkbench.CQRS.Query.Employee.Detail

{
    public record EmployeeDetailQuery(int Id) : IRequest<EmployeeResponse>;

    public class EmployeeDetailQueryValidator : AbstractValidator<EmployeeDetailQuery>
    {
        public EmployeeDetailQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}

