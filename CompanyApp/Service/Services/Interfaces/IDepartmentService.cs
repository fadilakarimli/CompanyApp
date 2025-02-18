using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task CreateAsync(Department department);
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int id);
        Task DeleteAsync(int id);
      //  Task UpdateAsync(int id, Department department);
        Task<IEnumerable<Department>> SearchAsync(string name);
    }
}
