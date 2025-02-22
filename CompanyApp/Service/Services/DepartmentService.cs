using Domain.Entities;
using Repository.Helpers;
using Repository.Helpers.Exceptions;
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
    public class DepartmentService : IDepartmentService
    {
        private readonly IDeparmentRepository _departmentRepo;
        public DepartmentService()
        {
            _departmentRepo = new DepartmentRepository();    
        }
        public async Task CreateAsync(Department department)
        {
            await _departmentRepo.CreateAsync(department);
        }
        public async Task DeleteAsync(int id)
        {
            var exsitDepartment = await _departmentRepo.GetByIdAsync(id);
            if (exsitDepartment == null)
            {
                throw new NotFoundException(ResponseMessages.NotFound);
            }
            await _departmentRepo.DeleteAsync(exsitDepartment);
        }
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _departmentRepo.GetAllAsync();
        }
        public async Task<Department> GetByIdAsync(int id)
        {
            return await _departmentRepo.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Department>> SearchAsync(string name)
        {
            return await _departmentRepo.SearchAsync(name);
        }
        public async  Task UpdateAsync(int id, Department department)
        {
            var existingDepartment = await _departmentRepo.GetByIdAsync(id);
            if (existingDepartment == null)
            {
                throw new NotFoundException(ResponseMessages.NotFound);
            }

            existingDepartment.Name = department.Name;
            existingDepartment.Capacity = department.Capacity;

            await _departmentRepo.UpdateAsync(existingDepartment);
        }
    }
}
