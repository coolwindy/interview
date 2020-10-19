using EmployeeApi.Models;
using EmployeeApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        Task<IEnumerable<Employee>> IEmployeeService.GetEmployeesWithTasksAsync()
        {
            return (Task<IEnumerable<Employee>>)employeeRepository.GetEmployeesWithTasks();
        }
    }
}
