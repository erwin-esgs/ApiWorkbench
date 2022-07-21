using System;
using System.ComponentModel.DataAnnotations;
namespace ApiWorkbench.Models
{
    public class BloodPressureRules : IRules
    {
        [Key]
        public int Id { set; get; }
        public string RuleName { set; get; }
        public string Expression { set; get; }
        public string SuccessEvent { set; get; }
    }
}

