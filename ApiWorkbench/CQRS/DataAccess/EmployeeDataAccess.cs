using System;
using ApiWorkbench.Models;
using ApiWorkbench.Properties;
using ApiWorkbench.CQRS.Command.Employee.Create;
using ApiWorkbench.CQRS.Command.Employee.Delete;
using ApiWorkbench.CQRS.Command.Employee.Edit;
using ApiWorkbench.CQRS.Query.Employee.Detail;
using ApiWorkbench.CQRS.Query.Employee.Show;
using ApiWorkbench.CQRS.Command.Employee;

namespace ApiWorkbench.CQRS.DataAccess
{
    public class EmployeeDataAccess : IEmployeeDataAccess
    {
        private readonly AppDbContext _dbContext;
        public EmployeeDataAccess() {

        }
        public EmployeeDataAccess(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EmployeeModel>> ShowEmployee() {
            var response = _dbContext.Employees.ToList();
            return await Task.FromResult(response);
        }

        public async Task<EmployeeModel> GetEmployee(int id)
        {
            var result = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(result);
        }

        public async Task<bool> AddEmployee( EmployeeAddCommand request )
        {

            EmployeeModel model = new();
            model.FirstName = request.FirstName;
            model.LastName = request.LastName;

            Console.WriteLine(_dbContext.Employees.Add(model));
            Console.WriteLine(_dbContext.SaveChanges());
            return await Task.FromResult(true);
        }

        public async Task<bool> EditEmployee( EmployeeEditCommand request )
        {
            var result = _dbContext.Employees.FirstOrDefault(x => x.Id == request.Id);
            if (result != null) {
                if (request.FirstName != null) result.FirstName = request.FirstName;
                if (request.LastName != null) result.LastName = request.LastName;
                Console.WriteLine(_dbContext.SaveChanges());
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
        public async Task<bool> DelEmployee(int id)
        {
            var result = await GetEmployee(id);
            if (result != null)
            {
                _dbContext.Employees.Attach(result);
                _dbContext.Employees.Remove(result);
                Console.WriteLine(_dbContext.SaveChanges());
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

    }
}

