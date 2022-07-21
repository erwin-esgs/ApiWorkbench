using System;
using MediatR;
using FluentValidation;
using ApiWorkbench.Models;
namespace ApiWorkbench.CQRS.Command.BloodPressure.Get
{
    public class BloodPressureCommand : IRequest<BloodPressureResponse>
    {
        public int Sistolik { get; set; }
        public int Diastolik { get; set; }
    }
    public class BloodPressureCommandValidator : AbstractValidator<BloodPressureCommand>
    {
        public BloodPressureCommandValidator()
        {
            RuleFor(x => x.Sistolik).NotEmpty().NotNull();
            RuleFor(x => x.Diastolik).NotEmpty().NotNull();
        }
    }
    public class BloodPressureResponse
    {
        public string Data { get; set; } = "false";
    }
}

