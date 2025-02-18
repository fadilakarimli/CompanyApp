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

        public async Task DeleteAsync(int id)
        {
           var exsitEmployee= await _employeeRepo.GetByIdAsync(id);
            await _employeeRepo.DeleteAsync(exsitEmployee);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
           return await _employeeRepo.GetAllAsync();
        }

        public Task<Employee> GetByIdAsync(int id)
        {
            return _employeeRepo.GetByIdAsync(id);
        }

        public Task UpdateAsync(int id, Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
