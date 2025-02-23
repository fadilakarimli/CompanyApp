using Domain.Entities;
using Repository.Helpers;
using Repository.Helpers.Constants;
using Repository.Helpers.Exceptions;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var employee = await _employeeRepo.GetByIdAsync(id);
            if (employee == null)
            {
                throw new NotFoundException(ResponseMessages.NotFound);
            }
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepo.GetAllAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var existEmployee = await _employeeRepo.GetByIdAsync(id);
            if (existEmployee == null)
            {
                throw new NotFoundException(ResponseMessages.NotFound);
            }
            await _employeeRepo.DeleteAsync(existEmployee);
        }

        public async Task<IEnumerable<Employee>> GetByAgeAsync(int age)
        {
            var employees = await _employeeRepo.GetByAgeAsync(age);

            if (employees == null || !employees.Any())
            {
                throw new NotFoundException(ResponseMessages.NotFound);
            }
            return employees;
        }

        public async Task<IEnumerable<Employee>> GetAllDepartmentNameAsync(string name)
        {
            var employees = await _employeeRepo.GetAllDepartmentNameAsync(name);

            if (employees == null || !employees.Any())
            {
                throw new NotFoundException(ResponseMessages.NotFound);
            }

            return employees;
        }

        public async Task<IEnumerable<Employee>> SearchAsync(string nameOrSurname)
        {
            var employees = await _employeeRepo.SearchAsync(nameOrSurname);

            if (employees == null || !employees.Any())
            {
                throw new NotFoundException(ResponseMessages.NotFound);
            }

            return employees;
        }

        public async Task<int> GetEmployeesCountAsync()
        {
            var count = await _employeeRepo.GetEmployeesCountAsync();

            if (count == 0)
            {
                throw new NotFoundException(ResponseMessages.NotFound);
            }

            return count;
        }

        public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId)
        {
            var employees = await _employeeRepo.GetByDepartmentIdAsync(departmentId);

            if (employees == null || !employees.Any())
            {
                throw new NotFoundException(ResponseMessages.NotFound);
            }

            return employees;
        }

        public async Task UpdateAsync(int id, Employee employee)
        {
            var existingEmployee = await _employeeRepo.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                throw new NotFoundException(ResponseMessages.NotFound);
            }

            if (!string.IsNullOrWhiteSpace(employee.Name))
            {
                if (!Regex.IsMatch(employee.Name, @"^[a-zA-Z\s]+$"))
                {
                    throw new ArgumentException("Name can only contain letters and spaces.");
                }
                existingEmployee.Name = employee.Name;
            }

            if (!string.IsNullOrWhiteSpace(employee.Surname))
            {
                if (!Regex.IsMatch(employee.Surname, @"^[a-zA-Z\s]+$"))
                {
                    throw new ArgumentException("Surname can only contain letters and spaces.");
                }
                existingEmployee.Surname = employee.Surname;
            }

            if (employee.Age >= 18)
            {
                existingEmployee.Age = employee.Age;
            }

            if (!string.IsNullOrWhiteSpace(employee.Address))
            {
                existingEmployee.Address = employee.Address;
            }

            if (employee.DepartmentId > 0)
            {
                existingEmployee.DepartmentId = employee.DepartmentId;
            }

            await _employeeRepo.UpdateAsync(existingEmployee);
            await _employeeRepo.SaveChangesAsync();
        }
    }
}
