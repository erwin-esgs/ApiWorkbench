using System;
using ApiWorkbench.Models;
using ApiWorkbench.Properties;
using ApiWorkbench.CQRS.Command.Employee.Create;
using ApiWorkbench.CQRS.Command.Employee.Delete;
using ApiWorkbench.CQRS.Command.Employee.Edit;
using ApiWorkbench.CQRS.Query.Employee.Detail;
using ApiWorkbench.CQRS.Query.Employee.Show;
using ApiWorkbench.CQRS.Command.Employee;
using ApiWorkbench.CQRS.DataAccess;

namespace ApiWorkbench.CQRS.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDataAccess _employeeDataAccess;
        public EmployeeService() {

        }
        public EmployeeService(IEmployeeDataAccess employeeDataAccess)
        {
            _employeeDataAccess = employeeDataAccess;
        }

        public async Task<List<EmployeeModel>> ShowEmployee() {
            return await _employeeDataAccess.ShowEmployee();
        }

        public async Task<EmployeeModel> GetEmployee(int id)
        {
            return await _employeeDataAccess.GetEmployee(id);
        }

        public async Task<bool> AddEmployee( EmployeeAddCommand request )
        {
            return await _employeeDataAccess.AddEmployee(request);
        }

        public async Task<bool> EditEmployee( EmployeeEditCommand request )
        {
            return await _employeeDataAccess.EditEmployee(request);
        }
        public async Task<bool> DelEmployee(int id)
        {
            return await _employeeDataAccess.DelEmployee(id);
        }

    }
}

