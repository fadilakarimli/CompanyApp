using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task CreateAsync(Employee employee);
        Task<Employee> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();
        //Task UpdateAsync(int id, Employee employee);
        Task DeleteAsync(int id);
        Task<IEnumerable<Employee>> GetByAgeAsync(int age);
        Task<IEnumerable<Employee>> GetAllDepartmentNameAsync();
        Task<IEnumerable<Employee>> SearchAsync(string nameOrSurname);
        Task<int> GetEmployeesCountAsync();

    }
}
