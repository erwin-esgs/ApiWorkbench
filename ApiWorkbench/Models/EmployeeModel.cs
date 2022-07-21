using System;
using System.ComponentModel.DataAnnotations;
namespace ApiWorkbench.Models
{
    public class EmployeeModel : IModel
    {
        [Key]
        public int Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
    }
}

