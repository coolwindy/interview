using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using EmployeeApi.Models;
using EmployeeApi.Services;
using EmployeeApi.Repositories;
using System.Threading.Tasks;
using System.Linq;

namespace EmployeeApi.Test.Controllers
{
    public class BaseServiceTests
    {
        private readonly BaseService<Employee> _baseService;
        private readonly Mock<IRepository<Employee>> _repositoryMock = new Mock<IRepository<Employee>>();
        private Random _random;

        public BaseServiceTests()
        {
            _random = new Random();
            _baseService = new BaseService<Employee>(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturn_WhenExists()
        {
            //Arrange
            int employeeId = _random.Next();
            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "San",
                LastName = "Zhang",
                HiredDate = DateTime.Today
            };
            
            _repositoryMock.Setup(x => x.GetById(employeeId)).Returns(employee);
            
            //Act
            var returnedEmployee = await _baseService.GetByIdAsync(employeeId);

            //Assert
            Assert.Equal(employeeId, returnedEmployee.Id);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAll()
        {
            //Arrange
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee { Id = _random.Next(), FirstName = "San", LastName = "Zhang", HiredDate = DateTime.Today });
            employees.Add(new Employee { Id = _random.Next(), FirstName = "Si", LastName = "Li", HiredDate = DateTime.Today });
            _repositoryMock.Setup(x => x.GetAll()).Returns(employees);

            //Act
            var returnedEmployees = await _baseService.GetAllAsync();

            //Assert
            Assert.Equal(returnedEmployees.ToList().Count, 2);
        }

        [Fact]
        public async Task UpdateAsync_ReturnNullIfPassNull()
        {
            var returnedEmployee = await _baseService.UpdateAsync(null);

            Assert.Null(returnedEmployee);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_IfNotExists()
        {
            int employeeId = _random.Next();
            string firstName = "San";
            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "San",
                LastName = "Zhang",
                HiredDate = DateTime.Today
            };
            _repositoryMock.Setup(x => x.IsExists(employeeId)).Returns(false);

            //Act
            Employee returnedEmployee = await _baseService.UpdateAsync(employee);

            Assert.Null(returnedEmployee);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedObject()
        {
            int employeeId = _random.Next();
            string firstName = "San";
            var employee = new Employee
            {
                Id = employeeId,
                FirstName = firstName,
                LastName = "Zhang",
                HiredDate = DateTime.Today
            };
            _repositoryMock.Setup(x => x.Update(employee)).Returns(employee);
            _repositoryMock.Setup(x => x.IsExists(employeeId)).Returns(true);

            //Act
            Employee returnedEmployee = await _baseService.UpdateAsync(employee);

            //Assert.Null(returnedEmployee);
            Assert.Equal(employeeId, returnedEmployee.Id);
            Assert.Equal(firstName, returnedEmployee.FirstName);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnNull_IfNotExists()
        {
            int employeeId = _random.Next();
            string firstName = "San";
            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "San",
                LastName = "Zhang",
                HiredDate = DateTime.Today
            };
            _repositoryMock.Setup(x => x.IsExists(employeeId)).Returns(false);

            //Act
            Employee returnedEmployee = await _baseService.DeleteAsync(employee.Id);

            Assert.Null(returnedEmployee);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnDeletedObject()
        {
            int employeeId = _random.Next();
            string firstName = "San";
            var employee = new Employee
            {
                Id = employeeId,
                FirstName = firstName,
                LastName = "Zhang",
                HiredDate = DateTime.Today
            };
            _repositoryMock.Setup(x => x.Delete(employee.Id)).Returns(employee);
            _repositoryMock.Setup(x => x.IsExists(employeeId)).Returns(true);

            //Act
            Employee returnedEmployee = await _baseService.DeleteAsync(employee.Id);

            //Assert.Null(returnedEmployee);
            Assert.Equal(employeeId, returnedEmployee.Id);
            Assert.Equal(firstName, returnedEmployee.FirstName);
        }

    }
}
