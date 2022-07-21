using System;
using Newtonsoft.Json;
namespace ApiWorkbench.Models
{
    public class Rules
    {
        public string RuleName { set; get; }
        public string Expression { set; get; }
        public string SuccessEvent { set; get; }
    }
    public class Workflow
    {
        public string WorkflowName { set; get; }
        public List<Rules> Rules { set; get; }
    }
    public class WorkflowRulesService
    {
        public static string ToWorkflowRule<T>(string name, List<T> items) where T : IRules
        {
            //List<Rules> rules = new List<Rules> { };
            //foreach (var item in items)
            //{
            //    rules.Add(new Rules() {
            //        RuleName = item?.RuleName,
            //        Expression = item?.Expression,
            //        SuccessEvent = item?.SuccessEvent
            //    });
            //}
            return JsonConvert.SerializeObject(new Workflow()
            {
                WorkflowName = name,
                Rules = items.Select(item => new Rules()
                {
                    RuleName = item?.RuleName,
                    Expression = item?.Expression,
                    SuccessEvent = item?.SuccessEvent
                }
                ).ToList()

                //rules
            }
            );
        }
    }
    
}

