using System;
using ApiWorkbench.Models;
namespace ApiWorkbench.CQRS.Command.Employee
{
    public class EmployeeListResponse
    {
        public List<EmployeeModel> Data { get; set; }
    }
}

