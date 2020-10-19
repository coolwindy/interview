using System;

namespace EmployeeApi.Models
{
    public class EmployeeTask : BaseEntity
    {
        public String TaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Deadline { get; set; }
        public int EmployeeId { get; set; }
    }
}
