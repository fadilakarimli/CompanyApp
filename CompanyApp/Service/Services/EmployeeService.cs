using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;
        public EmployeeService()
        {
            _employeeRepo = new EmployeeRepository();
        }
        public async Task CreateAsync(Employee employee)
        {
            await _employeeRepo.CreateAsync(employee);
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _employeeRepo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepo.GetAllAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existEmployee = await _employeeRepo.GetByIdAsync(id);
            if (existEmployee != null)
            {
                await _employeeRepo.DeleteAsync(existEmployee);
            }
        }

        public async Task<IEnumerable<Employee>> GetByAgeAsync(int age)
        {
            return await _employeeRepo.GetByAgeAsync(age);
        }

        public async Task<IEnumerable<Employee>> GetAllDepartmentNameAsync(string name)
        {
            return await _employeeRepo.GetAllDepartmentNameAsync(name);
        }

        public async Task<IEnumerable<Employee>> SearchAsync(string nameOrSurname)
        {
            return await _employeeRepo.SearchAsync(nameOrSurname);
        }

        public async Task<int> GetEmployeesCountAsync()
        {
            return await _employeeRepo.GetEmployeesCountAsync();
        }

        public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _employeeRepo.GetByDepartmentIdAsync(departmentId);
        }

    }
}
