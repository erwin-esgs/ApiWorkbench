using System;
using MediatR;
using RulesEngine.Models;
using Newtonsoft.Json;
using FluentValidation;
using ApiWorkbench.Properties;
using ApiWorkbench.Models;
namespace ApiWorkbench.CQRS.Command.BloodPressure.Get
{
    public class BloodPressureHandler : IRequestHandler<BloodPressureCommand , BloodPressureResponse>
    {
        private readonly string RULE_NAME = "BloodPressure";
        private readonly IEnumerable<IValidator<BloodPressureCommand>> _validators;
        private readonly RulesEngine.RulesEngine _rulesEngine;
        private readonly AppDbContext _dbContext;

        public BloodPressureHandler(
            IEnumerable<IValidator<BloodPressureCommand>> validators,
            AppDbContext dbContext
            )
        {
            _validators = validators;
            _dbContext = dbContext;

            var listRules = _dbContext.BloodPressureRules.ToList();
            string[] workflowRules = new string[] { WorkflowRulesService.ToWorkflowRule<BloodPressureRules>(RULE_NAME, listRules) };
            //foreach (var item in workflowRules)
            //{
            //    Console.WriteLine(item);
            //}
            _rulesEngine = new RulesEngine.RulesEngine(workflowRules, null);

        }

        public async Task<BloodPressureResponse> Handle(BloodPressureCommand request, CancellationToken cancellationToken)
        {

            BloodPressureResponse response = new();
            //foreach (var item in _validators) {
            //    Console.WriteLine($"{item}");
            //}
            var validator = _validators.First();
            var validate = validator.Validate(request);
            if (validate.IsValid)
            {
                var enumData = await EvaluateGiveParams(request);
                response.Data = enumData.ToString();
            }
            return response;
        }

        public async Task<BloodPressureType> EvaluateGiveParams(BloodPressureCommand request ) {
            //Console.WriteLine($"Pressure: {request.Sistolik} {request.Diastolik}");
            var ruleParam = new List<RuleParameter>() {
                new RuleParameter("Sistolik", request.Sistolik),
                new RuleParameter("Diastolik", request.Diastolik)
            };
            BloodPressureType @default = BloodPressureType.Normal;
            var result = await ExecuteWorkflowWithParams(RULE_NAME, ruleParam, @default);
            Console.WriteLine($"Result EvaluateAsync: {result} ");
            return result;
        }

        public async Task<T> ExecuteWorkflowWithParams<T>(string workflowName , List<RuleParameter> parameters, T @default = default)
        {
            var ruleResults = await _rulesEngine.ExecuteAllRulesAsync(workflowName, parameters.ToArray());
            var firstTrueResult = ruleResults.FirstOrDefault(x => x.IsSuccess);
            //Console.WriteLine(firstTrueResult?.Rule.SuccessEvent);
            return firstTrueResult != null ? Enums.ParseEnum<T>(firstTrueResult?.Rule?.SuccessEvent) : @default;
        }
    }
}

