using System;
using System.Collections.Generic;


namespace EmployeeApi.Models
{
    public class Employee : BaseEntity
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime HiredDate { get; set; }
        public virtual ICollection<EmployeeTask> employeeTasks { get; set; }
    }
}
