  using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public async Task<IEnumerable<Employee>> GetByAgeAsync(int age)
        {
            return await _context.Employees.Where(e => e.Age == age).ToListAsync();
        }
        public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _context.Employees
                .Include(e => e.Department) 
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Employee>> GetAllDepartmentNameAsync(string name)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Where(e => e.Department != null && e.Department.Name == name)
                .ToListAsync();
        }
        public async Task<IEnumerable<Employee>> SearchAsync(string key)
        {
            return await _context.Employees
                .Where(e => e.Name.Contains(key) || e.Surname.Contains(key))
                .ToListAsync();
        }
        public async Task<int> GetEmployeesCountAsync()
        {
            return await _context.Employees.CountAsync();
        }
    }
}
