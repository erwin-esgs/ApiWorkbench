using MediatR;
using FluentValidation;

namespace ApiWorkbench.CQRS.Command.Employee.Delete
{
    public class EmployeeDeleteCommand : IRequest<EmployeeListResponse> {
        public int Id {set;get;}
        public EmployeeDeleteCommand(int id)
        {
            Id = id;
        }
    }

    public class EmployeeDeleteCommandValidator : AbstractValidator<EmployeeDeleteCommand>
    {
        public EmployeeDeleteCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}

