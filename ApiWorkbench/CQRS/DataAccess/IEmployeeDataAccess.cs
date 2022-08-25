using System;
using ApiWorkbench.Models;
using ApiWorkbench.CQRS.Command.Employee;
using ApiWorkbench.CQRS.Command.Employee.Create;
using ApiWorkbench.CQRS.Command.Employee.Edit;
using ApiWorkbench.CQRS.Command.Employee.Delete;
using ApiWorkbench.CQRS.Query.Employee.Detail;
using ApiWorkbench.CQRS.Query.Employee.Show;

namespace ApiWorkbench.CQRS.DataAccess
{
    public interface IEmployeeDataAccess
    {
        public Task<List<EmployeeModel>> ShowEmployee();
        public Task<EmployeeModel> GetEmployee(int id);
        public Task<bool> AddEmployee(EmployeeAddCommand request);
        public Task<bool> EditEmployee(EmployeeEditCommand request);
        public Task<bool> DelEmployee(int id);
    }
}

