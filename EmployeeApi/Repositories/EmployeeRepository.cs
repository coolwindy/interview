using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DatabaseContext context) : base(context) { }

        public IEnumerable<Employee> GetEmployeesWithTasks()
        {
            return context.employees
                    .Include(e => e.employeeTasks).AsEnumerable();
        }
    }
}
