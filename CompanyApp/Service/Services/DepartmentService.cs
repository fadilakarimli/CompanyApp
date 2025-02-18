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
    public class DepartmentService : IDepartmentService
    {
        private readonly IDeparmentRepository _deparmentRepo;
        public DepartmentService()
        {
            _deparmentRepo = new DepartmentRepository();    
        }
        public async Task CreateAsync(Department department)
        {
            await _deparmentRepo.CreateAsync(department);
        }

        public async Task DeleteAsync(int id)
        {
            var exsitDepartment = await _deparmentRepo.GetByIdAsync(id);
            await _deparmentRepo.DeleteAsync(exsitDepartment);
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _deparmentRepo.GetAllAsync();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _deparmentRepo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Department>> SearchAsync(string name)
        {
            return await _deparmentRepo.SearchAsync(name);
        }

       
    }
}
