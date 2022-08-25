using System;
using Xunit;
using Xunit.Abstractions;
using Moq;
using Microsoft.EntityFrameworkCore.InMemory;
using ApiWorkbench.CQRS.Command.Employee.Create;
using ApiWorkbench.CQRS.Command.Employee.Delete;
using ApiWorkbench.CQRS.Command.Employee.Edit;
using ApiWorkbench.CQRS.Query.Employee.Detail;
using ApiWorkbench.CQRS.Query.Employee.Show;
using ApiWorkbench.CQRS.DataAccess;
using ApiWorkbench.CQRS.Service;
using ApiWorkbench.Properties;
using ApiWorkbench.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWorkbench.Test
{
    public class EmployeeTest : IDisposable
    {
        //private readonly EmployeeService _sut;
        //private readonly Mock<IEmployeeService> _employeeService = new Mock<IEmployeeService>();
        private readonly ITestOutputHelper output;
        public EmployeeTest()
        {
            //_sut = new EmployeeService(_employeeService.Object);
        }

        [Fact]
        public async Task Detail_Test() {

            //var EmployeeAddMock = new Mock<EmployeeAddCommand>();
            //var EmployeeEditMock = new Mock<EmployeeEditCommand>();
            //var EmployeeDeleteMock = new Mock<EmployeeDeleteCommand>();
            //var EmployeeDetailMock = new Mock<EmployeeDetailQuery>();
            //Console.WriteLine(EmployeeDetailMock.Object.Id);

            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            var employeeDataAccess = new Mock< IEmployeeDataAccess >();
            //employeeDataAccess.Setup(p => p.GetEmployee(1));
            //employeeDataAccess.As<AppDbContext>()
                //.Setup(p => p.GetEmployee(2)).Returns( Task.FromResult(new EmployeeModel {FirstName="erwin" , LastName="san" }) );
            //employeeDataAccess.Setup(p => p.AddEmployee(EmployeeAddMock.Object));
            
            //IEmployeeDataAccess employeeDataAccess1 = new EmployeeDataAccess();

            //var employeeService = new EmployeeService(employeeDataAccess.Object);
            var employeeService = new EmployeeService();
            var result = await employeeService.GetEmployee(1);
            //output.WriteLine(result.Id.ToString());

            employeeDataAccess.Verify(p => p.GetEmployee(1));
            Assert.Equal("erwin", result.FirstName);
        }
        [Fact]
        public async Task EmployeeAdd_Test()
        {

            //var EmployeeAddMock = new Mock<EmployeeAddCommand>();
            EmployeeAddCommand testRequest = new EmployeeAddCommand();
            testRequest.FirstName = "asd";
            testRequest.LastName = "asd";

            var employeeDataAccess = new Mock<IEmployeeDataAccess>();
            employeeDataAccess.Setup(p => p.AddEmployee(testRequest)).ReturnsAsync(true);//.Returns(Task.FromResult(true));
            var employeeService = new EmployeeService(employeeDataAccess.Object);
            
            var result = await employeeService.AddEmployee(testRequest);
            //var viewResult = Assert.IsType<bool>(result);
            Assert.True(result, "NOT TRUE");
            //employeeDataAccess.Verify();
        }

        [Fact]
        public async Task EmployeeEdit_Test()
        {

            var EmployeeEditMock = new Mock<EmployeeEditCommand>();

            var employeeDataAccess = new Mock<IEmployeeDataAccess>();

            var employeeService = new EmployeeService(employeeDataAccess.Object);
            employeeDataAccess.Setup(p => p.EditEmployee(EmployeeEditMock.Object)).ReturnsAsync(true);
            var result = await employeeService.EditEmployee(EmployeeEditMock.Object);

            Assert.True(result, "NOT TRUE");
        }



        public void Dispose()
        {
            // Do "global" teardown here; Only called once.
        }
    }

    public class DummyTests : IClassFixture<EmployeeTest>
    {
        public DummyTests(EmployeeTest data)
        {
        }
    }
}

