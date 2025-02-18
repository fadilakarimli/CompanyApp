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
    public class DepartmentRepository : BaseRepository<Department>, IDeparmentRepository
    {
        public async Task<IEnumerable<Department>> SearchAsync(string name)
        {
            return await _context.Departments.Where(d => d.Name.Contains(name)).ToListAsync();
        }
    }
}
