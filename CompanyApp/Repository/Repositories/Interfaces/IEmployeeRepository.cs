using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetByAgeAsync(int age);
        Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<Employee>> GetByDepartmentNameAsync(string departmentName);
        Task<IEnumerable<Employee>> SearchAsync(string key);
        Task<int> GetEmployeesCountAsync();
    }
}
