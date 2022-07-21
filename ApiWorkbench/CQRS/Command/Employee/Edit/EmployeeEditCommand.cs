using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace ApiWorkbench.CQRS.Command.Employee.Edit
{
    //public record EmployeeEditCommand (int _Id, string _FirstName = "", string _LastName = "") : IRequest<EmployeeListResponse>;

    public class EmployeeEditCommand : IRequest<EmployeeListResponse>
    {
        public int Id;
        public string FirstName;
        public string LastName;
    }

    public class EmployeeEditCommandValidator : AbstractValidator<EmployeeEditCommand>
    {
        public EmployeeEditCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            //RuleFor(x => x.FirstName).NotNull();
            //RuleFor(x => x.LastName).NotNull();
        }
    }

}

