using System.Collections.Generic;
using System.ServiceModel;

namespace HRService
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        ICollection<string> GetEmployeeList();

        [OperationContract]
        Employee GetEmployeeDetails(string alias);
    }
}
