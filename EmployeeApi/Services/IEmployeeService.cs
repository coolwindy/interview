﻿using EmployeeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Services
{
    public interface IEmployeeService : IService<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesWithTasksAsync();
    }
}
