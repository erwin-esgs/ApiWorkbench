using System;
namespace ApiWorkbench.Models
{
    public interface IRules
    {
         string RuleName { set; get; }
         string Expression { set; get; }
         string SuccessEvent { set; get; }
    }
}

