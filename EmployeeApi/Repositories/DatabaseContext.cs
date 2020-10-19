using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Employee> employees { get; set; }
        public DbSet<EmployeeTask> employeeTasks { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            //LoadDefaultEmployees();
        }

        private void LoadDefaultEmployees()
        {
            employees.Add(new Employee { FirstName = "San", LastName = "Zhang", HiredDate = DateTime.Today });
            employees.Add(new Employee { FirstName = "Si", LastName = "Li", HiredDate = DateTime.Today });
        }
    }
}
