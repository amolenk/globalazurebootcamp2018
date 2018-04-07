using System.Collections;
using System.Collections.Generic;

namespace HRService
{
    public class EmployeeService : IEmployeeService
    {
        public ICollection<string> GetEmployeeList()
        {
            return Employee.All.Keys;
        }

        public Employee GetEmployeeDetails(string alias)
        {
            return Employee.All[alias];
        }
    }
}
