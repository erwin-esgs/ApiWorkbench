using ApiWorkbench.Controllers.Employee;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ApiWorkbench.Properties;
using ApiWorkbench.Models;
using Autofac.Extras.Moq;
using 
namespace ApiWorkbench.Test;

public class UnitTest1 : IDisposable
{

    public UnitTest1()
    {
            
    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void TestControllerEmployee()
    {

        //new EmployeeDetailQuery(_id)
        //Mock<AppDbContext> mock = new Mock<AppDbContext>();
        //mock.Setup(x => x.Employees.ToList()).Returns(GetSampleEmployee());



        //using (var mock = AutoMock.GetLoose()) {
        //    mock.Mock<AppDbContext>().Setup(x => x.Employees.ToList()).Returns(GetSampleEmployee());
        //    var cls = mock.Create<EmployeeController>();
        //    var expected = GetSampleEmployee();
        //    var actual = cls.show();
        //    var okResult = actual.Result as OkObjectResult;
        //    List<EmployeeModel> results = (List<EmployeeModel>)okResult.Value;

        //    Assert.True(actual != null);
        //    Assert.Equal(expected.Count(), results.Count());
        //}

        //Arrange
        var expect = $"asd";
        //Act
        //EmployeeController employeeController = new();

        //var actual = employeeController.show();
        //Assert
        Assert.Equal(expect , "asd");
    }

    private List<EmployeeModel> GetSampleEmployee() {
        List<EmployeeModel> output = new List<EmployeeModel> {
            new EmployeeModel
            {
                Id = 1,
                FirstName = "Erwin",
                LastName = "San"
            },
            new EmployeeModel
            {
                Id = 2,
                FirstName = "qwe",
                LastName = "ewq"
            },
            new EmployeeModel
            {
                Id = 1,
                FirstName = "asd",
                LastName = "dsa"
            }
        };
        return output;
        
    }
}
