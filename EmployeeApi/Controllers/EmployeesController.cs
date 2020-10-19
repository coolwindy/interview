using EmployeeApi.Models;
using EmployeeApi.Repositories;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private IService<Employee> employeeService;

        public EmployeesController(IService<Employee> employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetEmployees()
        { 
            var employees = await employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await employeeService.GetByIdAsync(id);

            return employee != null ? (IActionResult)Ok(employee) : NotFound();
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            var newEmployee = await employeeService.InsertAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            var updatedEmployee = await employeeService.UpdateAsync(employee);
            if (updatedEmployee == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var deletedEmployee = await employeeService.DeleteAsync(id);
            if(deletedEmployee == null)
            {
                return NotFound();
            }

            return deletedEmployee;
        }
    }
}
