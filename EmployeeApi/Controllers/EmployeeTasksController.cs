using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApi.Models;
using EmployeeApi.Services;
using EmployeeApi.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeTasksController : ControllerBase
    {
        private IService<EmployeeTask> employeeTaskService;

        public EmployeeTasksController(IService<EmployeeTask> employeeTaskService)
        {
            this.employeeTaskService = employeeTaskService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetEmployeeTasks()
        {
            var employeeTasks = await employeeTaskService.GetAllAsync();
            return Ok(employeeTasks);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployeeTaskById(int id)
        {
            var employeeTask = await employeeTaskService.GetByIdAsync(id);

            return employeeTask != null ? (IActionResult)Ok(employeeTask) : NotFound();
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> AddEmployeeTask([FromBody] EmployeeTask employeeTask)
        {
            var newEmployeeTask = await employeeTaskService.InsertAsync(employeeTask);
            return CreatedAtAction(nameof(GetEmployeeTaskById), new { id = newEmployeeTask.Id }, newEmployeeTask);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateEmployeeTask([FromBody] EmployeeTask employeeTask)
        {
            var updatedEmployeeTask = await employeeTaskService.UpdateAsync(employeeTask);
            if (updatedEmployeeTask == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<EmployeeTask>> DeleteEmployeeTask(int id)
        {
            var deletedEmployeeTask = await employeeTaskService.DeleteAsync(id);
            if (deletedEmployeeTask == null)
            {
                return NotFound();
            }

            return deletedEmployeeTask;
        }
    }
}
